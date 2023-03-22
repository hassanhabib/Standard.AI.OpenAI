// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.NET.Brokers.HttpMessageHandlers;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Services.Foundations.Completions;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using RESTFulSense.Exceptions;

namespace OpenAI.NET.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
        private const string OpenAIApiConfigurationKey = nameof(OpenAIApiConfigurations);

        public OpenAIClient(OpenAIApiConfigurations apiConfigurations)
        {
            IHost host = RegisterServices(apiConfigurations);
            this.ApiConfigurations = apiConfigurations;
            InitializeClients(host);
        }

        internal OpenAIClient()
        {
            IHost host = RegisterServices();
            this.ApiConfigurations = host.Services.GetRequiredService<OpenAIApiConfigurations>();
            InitializeClients(host);
        }

        public ICompletionsClient Completions { get; set; }

        public OpenAIApiConfigurations ApiConfigurations { get; private set; }

        private void InitializeClients(IHost host) =>
            Completions = host.Services.GetRequiredService<ICompletionsClient>();

        private static IHost RegisterServices(OpenAIApiConfigurations apiConfigurations = null)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureAppConfiguration((ctx, builder) =>
            {
                var hostingEnvironment = ctx.HostingEnvironment;
                var contentPath = hostingEnvironment.ContentRootPath;
                var environmentName = hostingEnvironment.EnvironmentName;
                builder
                    .AddJsonFile(Path.Combine(contentPath, "appsettings.json"));
            });

            builder.ConfigureServices((ctx, services) =>
            {
                if (apiConfigurations is not null)
                {
                    AddBrokers(services, apiConfigurations);
                }
                else
                {
                    AddBrokers(services, ctx.Configuration, "OpenAIApiConfiguration");
                }

                services.AddTransient<ICompletionService, CompletionService>();
                services.AddTransient<ICompletionsClient, CompletionsClient>();
            });

            IHost host = builder.Build();

            return host;
        }

        /// <summary>
        /// Registers all the brokers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        private static IServiceCollection AddBrokers(
            IServiceCollection services,
            IConfiguration configuration,
            string openAIApiSettingsKey = null)
        {
            var apiConfigurations = GetOpenAIApiConfigurations(configuration, openAIApiSettingsKey);
            AddOpenAIBroker(services, apiConfigurations);
            return services;
        }

        /// <summary>
        /// Registers all the brokers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        private static IServiceCollection AddBrokers(
            IServiceCollection services,
            OpenAIApiConfigurations apiConfigurations)
        {
            AddOpenAIBroker(services, apiConfigurations);
            return services;
        }

        /// <summary>
        /// Registers all the dependencies for <see cref="OpenAIBroker"/> into the DI.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        private static IServiceCollection AddOpenAIBroker(
            IServiceCollection services,
            OpenAIApiConfigurations apiConfigurations)
        {
            services.AddSingleton(_ => apiConfigurations);
            var httpClientBuilder = services
                .AddHttpClient<OpenAIBroker>(httpClient =>
                    httpClient.BaseAddress = new(uriString: apiConfigurations.ApiUrl))
                .AddHttpMessageHandler<OpenAIBrokerDeligatingHandler>();

            if (apiConfigurations.PolicySettings is not null)
            {
                httpClientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
                    CreatePolicies(policyBuilder, apiConfigurations.PolicySettings));
            }
                
            services.AddScoped(_ => new OpenAIBrokerDeligatingHandler(apiConfigurations));

            services.AddScoped<IOpenAIBroker, OpenAIBroker>();

            return services;
        }

        private static OpenAIApiConfigurations GetOpenAIApiConfigurations(
            IConfiguration configuration,
            string openAIApiSettingsKey = null)
        {
            openAIApiSettingsKey ??= OpenAIApiConfigurationKey;
            OpenAIApiConfigurations apiConfigurations = configuration
                                                            .GetSection(openAIApiSettingsKey)
                                                            .Get<OpenAIApiConfigurations>();
            return apiConfigurations;
        }

        private static IAsyncPolicy<HttpResponseMessage> CreatePolicies(
            PolicyBuilder<HttpResponseMessage> policyBuilder,
            HttpPolicySettings policySettings)
        {
            var timeOut = ExtractTimeSpan(policySettings.Timeout);
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(timeOut);

            AsyncRetryPolicy<HttpResponseMessage> retryPolicy = null;
            AsyncCircuitBreakerPolicy<HttpResponseMessage> circuitBreakerPolicy = null;

            if (policySettings.EnableRetry && policySettings.NumberOfRetries > 0)
            {
                policyBuilder = policyBuilder
                                .Or<TimeoutException>()
                                .Or<TimeoutRejectedException>()
                                .Or<HttpResponseRequestTimeoutException>()
                                .Or<HttpResponseGatewayTimeoutException>();

                if (!policySettings.WaitingAmongRetries.Any())
                {
                    retryPolicy = policyBuilder.RetryAsync(policySettings.NumberOfRetries);
                }
                else
                {
                    var waitingTimes = policySettings
                                            .WaitingAmongRetries
                                            .Select(x => ExtractTimeSpan(x))
                                            .ToList();
                    retryPolicy = policyBuilder.WaitAndRetryAsync(waitingTimes);
                }

                if (policySettings.EnableCircuitBreaker)
                {
                    circuitBreakerPolicy = policyBuilder.CircuitBreakerAsync(policySettings.MaxNumberOffailures, ExtractTimeSpan(policySettings.BreakCircuitFor));
                }
            }

            if (retryPolicy is null)
            {
                return timeoutPolicy;
            }

            if (circuitBreakerPolicy is null)
            {
                return Policy.WrapAsync(retryPolicy, timeoutPolicy);
            }

            return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
        }

        private static TimeSpan ExtractTimeSpan(object duration)
        {
            if (duration is TimeSpan durationTs && durationTs > TimeSpan.Zero)
            {
                return durationTs;
            }

            if (duration is int durationInt && durationInt > 0)
            {
                return TimeSpan.FromSeconds(durationInt);
            }

            var durationStr = duration.ToString();

            if (string.IsNullOrWhiteSpace(durationStr))
            {
                throw new Exception("Invalid value provided. Either provide a number or string (3s, 3ms).");
            }

            return durationStr switch
            {
                string _ when int.TryParse(durationStr, out int durationMsInt) =>
                    TimeSpan.FromSeconds(durationMsInt),
                string _ when durationStr.EndsWith("ms", StringComparison.InvariantCultureIgnoreCase) =>
                    TimeSpan.FromMilliseconds(double.Parse(durationStr[..^2])),
                string _ when durationStr.EndsWith("s", StringComparison.InvariantCultureIgnoreCase) =>
                    TimeSpan.FromSeconds(double.Parse(durationStr[..^1])),
                _ => throw new Exception("Invalid value unit. Allowed units are seconds and milliseconds.")
            };
        }
    }
}
