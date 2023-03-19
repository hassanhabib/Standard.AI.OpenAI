// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Linq;
using OpenAI.NET.Clients.OpenAIs;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Configurations;
using OpenAI.NET.Models.ExternalCompletions;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace OpenAI.NET.Tests.Acceptance.Clients.Completions
{
    public partial class CompletionClientTests : IDisposable
    {
        private readonly IOpenAIClient openAIClient;
        private readonly WireMockServer wireMockServer;
        private readonly string apiKey;
        private readonly string organizationId;

        public CompletionClientTests()
        {
            this.wireMockServer = WireMockServer.Start(1989);
            this.apiKey = CreateRandomString();

            var openAiConfiguration = new ApiConfigurations
            {
                ApiUrl = "http://localhost:1989",
                ApiKey = this.apiKey,
                OrganizationId = this.organizationId
            };

            this.openAIClient = new OpenAIClient(openAiConfiguration);
        }

        private static ExternalCompletionRequest ConvertToCompletionRequest(Completion completion)
        {
            return new ExternalCompletionRequest
            {
                Model = completion.Request.Model,
                BestOf = completion.Request.BestOf,
                CompletionsPerPrompt = completion.Request.CompletionsPerPrompt,
                Echo = completion.Request.Echo,
                FrequencyPenalty = completion.Request.FrequencyPenalty,
                LogitBias = completion.Request.LogitBias,
                LogProbabilities = completion.Request.LogProbabilities,
                MaxTokens = completion.Request.MaxTokens,
                PresencePenalty = completion.Request.PresencePenalty,
                ProbabilityMass = completion.Request.ProbabilityMass,
                Prompt = completion.Request.Prompt,
                Stop = completion.Request.Stop,
                Stream = completion.Request.Stream,
                Suffix = completion.Request.Suffix,
                Temperature = completion.Request.Temperature,
                User = completion.Request.User
            };
        }

        private static Completion ConvertToCompletion(
           Completion completion,
           ExternalCompletionResponse externalCompletionResponse)
        {
            completion.Response = new CompletionResponse
            {
                Model = externalCompletionResponse.Model,

                Choices = externalCompletionResponse.Choices.Select(externalChoice => new Choice
                {
                    FinishReason = externalChoice.FinishReason,
                    Index = externalChoice.Index,
                    LogProbabilities = externalChoice.LogProbabilities,
                    Text = externalChoice.Text
                }).ToArray(),

                Created = externalCompletionResponse.Created,
                Id = externalCompletionResponse.Id,

                Usage = new Usage
                {
                    CompletionTokens = externalCompletionResponse.Usage.CompletionTokens,
                    PromptTokens = externalCompletionResponse.Usage.PromptTokens,
                    TotalTokens = externalCompletionResponse.Usage.TotalTokens
                },

                Object = externalCompletionResponse.Object
            };

            return completion;
        }

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static Completion CreateRandomCompletion() =>
            CreateCompletionFiller().Create();

        private static ExternalCompletionResponse CreateRandomExternalCompletionResponse() =>
            CreateCompletionResponseFiller().Create();

        private static Filler<ExternalCompletionResponse> CreateCompletionResponseFiller()
        {
            var filler = new Filler<ExternalCompletionResponse>();

            filler.Setup()
               .OnType<object>().IgnoreIt();

            return filler;
        }

        private static Filler<Completion> CreateCompletionFiller()
        {
            var filler = new Filler<Completion>();

            filler.Setup()
                .OnType<object>().IgnoreIt();

            return filler;
        }

        public void Dispose()
        {
            this.wireMockServer.Reset();
            this.wireMockServer.Stop();
        }
    }
}
