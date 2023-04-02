// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.ChatCompletions
{
    public partial class ChatCompletionClientTests : IDisposable
    {
        private readonly IOpenAIClient openAIClient;
        private readonly WireMockServer wireMockServer;
        private readonly string apiKey;
        private readonly string organizationId;

        public ChatCompletionClientTests()
        {
            this.wireMockServer = WireMockServer.Start();
            this.apiKey = CreateRandomString();
            this.organizationId = CreateRandomString();

            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = this.apiKey,
                OrganizationId = this.organizationId
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }

        private static ExternalChatCompletionRequest ConvertToChatCompletionRequest(ChatCompletion chatCompletion)
        {
            return new ExternalChatCompletionRequest
            {
                Model = chatCompletion.Request.Model,

                Messages = chatCompletion.Request.Messages.Select(message =>
                {
                    return new ExternalChatCompletionMessage
                    {
                        Role = message.Role,
                        Content = message.Content
                    };
                }).ToArray(),

                Temperature = chatCompletion.Request.Temperature,
                ProbabilityMass = chatCompletion.Request.ProbabilityMass,
                CompletionsPerPrompt = chatCompletion.Request.CompletionsPerPrompt,
                Stream = chatCompletion.Request.Stream,
                Stop = chatCompletion.Request.Stop,
                MaxTokens = chatCompletion.Request.MaxTokens,
                PresencePenalty = chatCompletion.Request.PresencePenalty,
                FrequencyPenalty = chatCompletion.Request.FrequencyPenalty,
                LogitBias = chatCompletion.Request.LogitBias,
                User = chatCompletion.Request.User
            };
        }

        private static ChatCompletionMessage ConvertToExternalChatCompletionMessage(
            ExternalChatCompletionMessage chatCompletionMessage)
        {
            return new ChatCompletionMessage
            {
                Content = chatCompletionMessage.Content,
                Role = chatCompletionMessage.Role,
            };
        }

        private static ChatCompletion ConvertToChatCompletion(
           ChatCompletion chatCompletion,
           ExternalChatCompletionResponse externalChatCompletionResponse)
        {
            chatCompletion.Response = new ChatCompletionResponse
            {

                Id = externalChatCompletionResponse.Id,
                Object = externalChatCompletionResponse.Object,
                CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalChatCompletionResponse.Created),

                Choices = externalChatCompletionResponse.Choices.Select(externalChoice => new ChatCompletionChoice
                {
                    FinishReason = externalChoice.FinishReason,
                    Message = ConvertToExternalChatCompletionMessage(externalChoice.Message),
                    Index = externalChoice.Index,
                }).ToArray(),


                Usage = new ChatCompletionUsage
                {
                    CompletionTokens = externalChatCompletionResponse.Usage.CompletionTokens,
                    PromptTokens = externalChatCompletionResponse.Usage.PromptTokens,
                    TotalTokens = externalChatCompletionResponse.Usage.TotalTokens
                },

            };

            return chatCompletion;
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static ChatCompletion CreateRandomChatCompletion() =>
            CreateChatCompletionFiller().Create();

        private static ExternalChatCompletionResponse CreateRandomExternalChatCompletionResponse() =>
            CreateChatCompletionResponseFiller().Create();

        private static Filler<ExternalChatCompletionResponse> CreateChatCompletionResponseFiller()
        {
            var filler = new Filler<ExternalChatCompletionResponse>();

            filler.Setup()
               .OnType<object>().IgnoreIt();

            return filler;
        }

        private static Filler<ChatCompletion> CreateChatCompletionFiller()
        {
            var filler = new Filler<ChatCompletion>();

            filler.Setup()
                .OnType<object>().IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
