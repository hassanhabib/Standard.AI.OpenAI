// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Clients.Completions;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Services.Foundations.Completions;

namespace Standard.AI.OpenAI.Clients.OpenAIs
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
