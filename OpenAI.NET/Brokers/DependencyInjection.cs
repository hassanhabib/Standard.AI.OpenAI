using Microsoft.Extensions.DependencyInjection;

using OpenAI.NET.Brokers.HttpMessageHandlers;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Models.Configurations;

using RESTFulSense.Clients;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.NET.Brokers
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddBrokers(
            this IServiceCollection services)
        {
            services
                .AddOpenAIBrokerHttpClient()
                .AddScoped<IOpenAIBroker, OpenAIBroker>();

            return services;
        }

        private static IServiceCollection AddOpenAIBrokerHttpClient(
            this IServiceCollection services)
        {
            services
                .AddHttpClient<OpenAIBroker>((configuration, httpClient) =>
                {
                    ApiConfigurations apiConfigurations = configuration.GetRequiredService<ApiConfigurations>();
                    httpClient.BaseAddress = new Uri(uriString: apiConfigurations.ApiUrl);
                })
                .AddHttpMessageHandler<OpenAIBrokerAuthorizationMessageHandler>()
                .Services
                .AddScoped<OpenAIBrokerAuthorizationMessageHandler>();

            return services;
        }
    }
}
