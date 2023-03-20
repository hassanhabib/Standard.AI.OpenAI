using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.NET.Brokers;

namespace OpenAI.NET.Clients.OpenAIs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenApiClient(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddBrokers(configuration, "OpenAIApiContiguration");
            services.AddScoped<IOpenAIClient>(_ => new OpenAIClient());
            return services;
        }
    }
}
