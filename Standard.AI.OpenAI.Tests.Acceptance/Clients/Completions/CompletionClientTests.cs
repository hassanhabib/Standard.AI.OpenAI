// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.Completions
{
    public partial class CompletionClientTests : IDisposable
    {
        private readonly IOpenAIClient openAIClient;
        private readonly WireMockServer wireMockServer;
        private readonly string apiKey;
        private readonly string organizationId;

        public CompletionClientTests()
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

        private static ExternalCompletionRequest ConvertToCompletionRequest(Completion completion)
        {
            return new ExternalCompletionRequest
            {
                Model = completion.Request.Model,
                Prompts = completion.Request.Prompts,
                Suffix = completion.Request.Suffix,
                MaxTokens = completion.Request.MaxTokens,
                Temperature = completion.Request.Temperature,
                ProbabilityMass = completion.Request.ProbabilityMass,
                CompletionsPerPrompt = completion.Request.CompletionsPerPrompt,
                Stream = completion.Request.Stream,
                LogProbabilities = completion.Request.LogProbabilities,
                Echo = completion.Request.Echo,
                Stop = completion.Request.Stop,
                PresencePenalty = completion.Request.PresencePenalty,
                FrequencyPenalty = completion.Request.FrequencyPenalty,
                BestOf = completion.Request.BestOf,
                LogitBias = completion.Request.LogitBias,
                User = completion.Request.User
            };
        }

        private static Completion ConvertToCompletion(
           Completion completion,
           ExternalCompletionResponse externalCompletionResponse)
        {
            completion.Response = new CompletionResponse
            {
                Id = externalCompletionResponse.Id,
                Object = externalCompletionResponse.Object,
                CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalCompletionResponse.Created),
                Model = externalCompletionResponse.Model,

                Choices = externalCompletionResponse.Choices.Select(externalChoice => new Choice
                {
                    FinishReason = externalChoice.FinishReason,
                    Index = externalChoice.Index,
                    LogProbabilities = externalChoice.LogProbabilities,
                    Text = externalChoice.Text
                }).ToArray(),

                Usage = new Usage
                {
                    CompletionTokens = externalCompletionResponse.Usage.CompletionTokens,
                    PromptTokens = externalCompletionResponse.Usage.PromptTokens,
                    TotalTokens = externalCompletionResponse.Usage.TotalTokens
                }
            };

            return completion;
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

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
                .OnType<object>().IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
