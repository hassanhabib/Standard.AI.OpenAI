// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Clients.ChatCompletions;
using Standard.AI.OpenAI.Clients.Completions;
using Standard.AI.OpenAI.Clients.ImageGenerations;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Services.Foundations.Completions;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
        public OpenAIClient(ApiConfigurations apiConfigurations)
        {
            IServiceProvider serviceProvider = RegisterServices(apiConfigurations);
            InitializeClients(serviceProvider);
        }

        public ICompletionsClient Completions { get; private set; }
        public IChatCompletionsClient ChatCompletions { get; private set; }
        public IImageGenerationsClient ImageGenerations { get; private set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            Completions = serviceProvider.GetRequiredService<ICompletionsClient>();
            ChatCompletions = serviceProvider.GetRequiredService<IChatCompletionsClient>();
            ImageGenerations = serviceProvider.GetRequiredService<IImageGenerationsClient>();
        }

        private static IServiceProvider RegisterServices(ApiConfigurations apiConfigurations)
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IOpenAIBroker, OpenAIBroker>()
                .AddTransient<ICompletionService, CompletionService>()
                .AddTransient<IChatCompletionService, ChatCompletionService>()
                .AddTransient<IImageGenerationService, ImageGenerationService>()
                .AddTransient<ICompletionsClient, CompletionsClient>()
                .AddTransient<IChatCompletionsClient, ChatCompletionsClient>()
                .AddTransient<IImageGenerationsClient, ImageGenerationsClient>()
                .AddSingleton(apiConfigurations);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
