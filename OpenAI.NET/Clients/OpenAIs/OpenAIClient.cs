// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OpenAI.NET.Brokers;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Services.Foundations.Completions;

namespace OpenAI.NET.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAIClient"/> class.
        /// </summary>
        /// <param name="apiConfigurations">The api configurations.</param>
        public OpenAIClient(ApiConfigurations apiConfigurations)
        {
            IHost host = RegisterServices(apiConfigurations);
            InitializeClients(host);
        }

        public ICompletionsClient Completions { get; set; }

        private void InitializeClients(IHost host) =>
            Completions = host.Services.GetRequiredService<ICompletionsClient>();

        private static IHost RegisterServices(ApiConfigurations apiConfigurations)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(configuration =>
            {
                configuration.AddSingleton(_ => apiConfigurations);
                configuration.AddBrokers();
                configuration.AddTransient<ICompletionService, CompletionService>();
                configuration.AddTransient<ICompletionsClient, CompletionsClient>();
            });

            IHost host = builder.Build();
            
            return host;
        }

    }
}
