// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.Files;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Clients.AIFiles;
using Standard.AI.OpenAI.Clients.AIModels;
using Standard.AI.OpenAI.Clients.ChatCompletions;
using Standard.AI.OpenAI.Clients.Completions;
using Standard.AI.OpenAI.Clients.FineTunes;
using Standard.AI.OpenAI.Clients.ImageGenerations;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Services.Foundations.Completions;
using Standard.AI.OpenAI.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;
using Standard.AI.OpenAI.Services.Orchestrations.AIFiles;

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
        public IAIModelsClient AIModels { get; private set; }
        public IAIFilesClient AIFiles { get; private set; }
        public IFineTuneClient FineTuneClient { get; set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            Completions = serviceProvider.GetRequiredService<ICompletionsClient>();
            ChatCompletions = serviceProvider.GetRequiredService<IChatCompletionsClient>();
            ImageGenerations = serviceProvider.GetRequiredService<IImageGenerationsClient>();
            AIModels = serviceProvider.GetRequiredService<IAIModelsClient>();
            AIFiles = serviceProvider.GetRequiredService<IAIFilesClient>();
            FineTuneClient = serviceProvider.GetService<IFineTuneClient>();
        }

        private static IServiceProvider RegisterServices(OpenAIConfigurations openAIConfigurations)
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IOpenAIBroker, OpenAIBroker>()
                .AddTransient<IDateTimeBroker, DateTimeBroker>()
                .AddTransient<IFileBroker, FileBroker>()
                .AddTransient<ICompletionService, CompletionService>()
                .AddTransient<IChatCompletionService, ChatCompletionService>()
                .AddTransient<IImageGenerationService, ImageGenerationService>()
                .AddTransient<IAIModelService, AIModelService>()
                .AddTransient<IAudioTranscriptionService, AudioTranscriptionService>()
                .AddTransient<ILocalFileService, LocalFileService>()
                .AddTransient<IAIFileService, AIFileService>()
                .AddTransient<IFineTuneService, FineTuneService>()
                .AddTransient<IAIFileOrchestrationService, AIFileOrchestrationService>()
                .AddTransient<ICompletionsClient, CompletionsClient>()
                .AddTransient<IChatCompletionsClient, ChatCompletionsClient>()
                .AddTransient<IImageGenerationsClient, ImageGenerationsClient>()
                .AddTransient<IAIModelsClient, AIModelsClient>()
                .AddTransient<IAIFilesClient, AIFilesClient>()
                .AddTransient<IFineTuneClient, FineTuneClient>()
                .AddSingleton(openAIConfigurations);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
