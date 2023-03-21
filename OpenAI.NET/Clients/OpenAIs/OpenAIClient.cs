// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Clients.Completions;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Services.Foundations.Completions;

namespace OpenAI.NET.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
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
                configuration.AddTransient<IOpenAIBroker, OpenAIBroker>();
                configuration.AddTransient<ICompletionService, CompletionService>();
                configuration.AddTransient<ICompletionsClient, CompletionsClient>();
                configuration.AddSingleton(options => apiConfigurations);
            });

            IHost host = builder.Build();

            return host;
        }
    }
}
