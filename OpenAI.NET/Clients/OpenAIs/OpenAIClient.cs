// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.NET.Brokers.HttpMessageHandlers;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Services.Foundations.Completions;

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
            services
                .AddHttpClient<OpenAIBroker>(httpClient =>
                    httpClient.BaseAddress = new(uriString: apiConfigurations.ApiUrl))
                .AddHttpMessageHandler<OpenAIBrokerDeligatingHandler>()
                .Services
                .AddScoped(_ => new OpenAIBrokerDeligatingHandler(apiConfigurations));

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
    }
}
