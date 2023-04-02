// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Clients.AIModels;
using Standard.AI.OpenAI.Clients.AudioTranscriptions;
using Standard.AI.OpenAI.Clients.ChatCompletions;
using Standard.AI.OpenAI.Clients.Completions;
using Standard.AI.OpenAI.Clients.ImageGenerations;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Services.Foundations.Completions;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Clients.OpenAIs
{
    public class OpenAIClient : IOpenAIClient
    {
        public OpenAIClient(OpenAIConfigurations openAIConfigurations)
        {
            IServiceProvider serviceProvider = RegisterServices(openAIConfigurations);
            InitializeClients(serviceProvider);
        }

        public ICompletionsClient Completions { get; private set; }
        public IChatCompletionsClient ChatCompletions { get; private set; }
        public IImageGenerationsClient ImageGenerations { get; private set; }
        public IAIModelsClient AllAIModels { get; private set; }
        public IAudioTranscriptionsClient AudioTranscriptions { get; private set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            Completions = serviceProvider.GetRequiredService<ICompletionsClient>();
            ChatCompletions = serviceProvider.GetRequiredService<IChatCompletionsClient>();
            ImageGenerations = serviceProvider.GetRequiredService<IImageGenerationsClient>();
            AllAIModels = serviceProvider.GetRequiredService<IAIModelsClient>();
            AudioTranscriptions = serviceProvider.GetRequiredService<IAudioTranscriptionsClient>();
        }

        private static IServiceProvider RegisterServices(OpenAIConfigurations openAIConfigurations)
        {
            var serviceCollection = new ServiceCollection()
                // Brokers
                .AddTransient<IOpenAIBroker, OpenAIBroker>()
                .AddTransient<IDateTimeBroker, DateTimeBroker>()
                // Services
                .AddTransient<ICompletionService, CompletionService>()
                .AddTransient<IChatCompletionService, ChatCompletionService>()
                .AddTransient<IImageGenerationService, ImageGenerationService>()
                .AddTransient<IAIModelService, AIModelService>()
                .AddTransient<IAudioTranscriptionService, AudioTranscriptionService>()
                // Clients
                .AddTransient<ICompletionsClient, CompletionsClient>()
                .AddTransient<IChatCompletionsClient, ChatCompletionsClient>()
                .AddTransient<IImageGenerationsClient, ImageGenerationsClient>()
                .AddTransient<IAIModelsClient, AIModelsClient>()
                .AddTransient<IAudioTranscriptionsClient, AudioTranscriptionsClient>()
                .AddSingleton(openAIConfigurations);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
