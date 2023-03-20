using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Models.Configurations;

namespace OpenAI.NET.Brokers
{
    /// <summary>
    /// The dependency injection helper for brokers.
    /// </summary>
    internal static class DependencyInjection
    {
        private const string OpenAIApiConfigurationKey = nameof(OpenAIApiConfigurations);

        /// <summary>
        /// Registers all the brokers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddBrokers(
            this IServiceCollection services,
            IConfiguration configuration,
            string openAIApiSettingsKey = null)
        {
            var apiConfigurations = configuration.GetOpenAIApiConfigurations(openAIApiSettingsKey);
            services.AddOpenAIBroker(apiConfigurations);
            return services;
        }

        /// <summary>
        /// Registers all the brokers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddBrokers(
            this IServiceCollection services,
            OpenAIApiConfigurations apiConfigurations)
        {
            services.AddOpenAIBroker(apiConfigurations);
            return services;
        }

        /// <summary>
        /// Registers all the dependencies for <see cref="OpenAIBroker"/> into the DI.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        private static IServiceCollection AddOpenAIBroker(
            this IServiceCollection services,
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
            this IConfiguration configuration,
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
