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
        /// <summary>
        /// Registers all the brokers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddBrokers(
            this IServiceCollection services)
        {
            services.AddOpenAIBroker();
            return services;
        }

        /// <summary>
        /// Registers all the dependencies for <see cref="OpenAIBroker"/> into the DI.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        private static IServiceCollection AddOpenAIBroker(
            this IServiceCollection services)
        {
            services.AddHttpClient<OpenAIBroker>((configuration, httpClient) =>
            {
                var apiCOnfigurations = configuration.GetRequiredService<ApiConfigurations>();
                httpClient.BaseAddress = new(uriString: apiCOnfigurations.ApiUrl);
            })
            .AddHttpMessageHandler<OpenAIBrokerDeligatingHandler>();

            services.AddScoped<IOpenAIBroker, OpenAIBroker>();

            return services;
        }
    }
}
