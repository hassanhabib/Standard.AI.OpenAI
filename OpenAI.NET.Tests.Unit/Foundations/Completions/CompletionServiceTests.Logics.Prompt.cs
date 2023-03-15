// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.ExternalCompletions;
using Xunit;

namespace OpenAI.NET.Tests.Unit.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Fact]
        public async Task PromptCompletionAsync()
        {
            // given
            dynamic randomCompletionProperties = CreateRandomCompletionProperties();

            var randomCompletionRequest = new CompletionRequest
            {
                Model = randomCompletionProperties.RequestModel,
                Prompt = randomCompletionProperties.Prompt,
                Suffix = randomCompletionProperties.Suffix,
                MaxTokens = randomCompletionProperties.MaxTokens,
                Temperature = randomCompletionProperties.Temperature,
                ProbabilityMass = randomCompletionProperties.ProbabilityMass,
                CompletionsPerPrompt = randomCompletionProperties.CompletionsPerPrompt,
                Stream = randomCompletionProperties.Stream,
                LogProbabilities = randomCompletionProperties.LogProbabilities,
                Echo = randomCompletionProperties.Echo,
                Stop = randomCompletionProperties.Stop,
                PresencePenalty = randomCompletionProperties.PresencePenalty,
                FrequencyPenalty = randomCompletionProperties.FrequencyPenalty,
                BestOf = randomCompletionProperties.BestOf,
                LogitBias = randomCompletionProperties.LogitBias,
                User = randomCompletionProperties.User
            };

            var randomCompletionResponse = new CompletionResponse
            {
                Id = randomCompletionProperties.Id,
                Object = randomCompletionProperties.Object,
                Created = randomCompletionProperties.Created,
                Model = randomCompletionProperties.ResponseModel,

                Choices = ((dynamic[])randomCompletionProperties.Choices).Select(item =>
                    new Choice
                    {
                        FinishReason = item.FinishReason,
                        Index = item.Index,
                        LogProbabilities = item.LogProbabilities,
                        Text = item.Text
                    }).ToArray(),

                Usage = new Usage
                {
                    CompletionTokens = randomCompletionProperties.Usage.CompletionTokens,
                    PromptTokens = randomCompletionProperties.Usage.PromptTokens,
                    TotalTokens = randomCompletionProperties.Usage.TotalTokens
                }
            };

            var randomCompletion = new Completion
            {
                Request = randomCompletionRequest,
                Response = randomCompletionResponse
            };

            var randomExternalCompletionRequest = new ExternalCompletionRequest
            {
                Model = randomCompletionProperties.RequestModel,
                Prompt = randomCompletionProperties.Prompt,
                Suffix = randomCompletionProperties.Suffix,
                MaxTokens = randomCompletionProperties.MaxTokens,
                Temperature = randomCompletionProperties.Temperature,
                ProbabilityMass = randomCompletionProperties.ProbabilityMass,
                CompletionsPerPrompt = randomCompletionProperties.CompletionsPerPrompt,
                Stream = randomCompletionProperties.Stream,
                LogProbabilities = randomCompletionProperties.LogProbabilities,
                Echo = randomCompletionProperties.Echo,
                Stop = randomCompletionProperties.Stop,
                PresencePenalty = randomCompletionProperties.PresencePenalty,
                FrequencyPenalty = randomCompletionProperties.FrequencyPenalty,
                BestOf = randomCompletionProperties.BestOf,
                LogitBias = randomCompletionProperties.LogitBias,
                User = randomCompletionProperties.User
            };

            var randomExternalCompletionResponse = new ExternalCompletionResponse
            {
                Id = randomCompletionProperties.Id,
                Object = randomCompletionProperties.Object,
                Created = randomCompletionProperties.Created,
                Model = randomCompletionProperties.ResponseModel,

                Choices = ((dynamic[])randomCompletionProperties.Choices).Select(item =>
                    new ExternalChoice
                    {
                        FinishReason = item.FinishReason,
                        Index = item.Index,
                        LogProbabilities = item.LogProbabilities,
                        Text = item.Text
                    }).ToArray(),

                Usage = new ExternalUsage
                {
                    CompletionTokens = randomCompletionProperties.Usage.CompletionTokens,
                    PromptTokens = randomCompletionProperties.Usage.PromptTokens,
                    TotalTokens = randomCompletionProperties.Usage.TotalTokens
                }
            };

            Completion inputCompletion = randomCompletion;
            Completion expectedCompletion = inputCompletion.DeepClone();
            ExternalCompletionRequest mappedExternalCompletionRequest = randomExternalCompletionRequest;
            ExternalCompletionResponse returnedExternalCompletionResponse = randomExternalCompletionResponse;

            this.openAiBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(It.Is(
                    SameExternalCompletionRequestAs(mappedExternalCompletionRequest))))
                        .ReturnsAsync(returnedExternalCompletionResponse);

            // when
            Completion actualCompletion = await this.completionService.PromptCompletionAsync(inputCompletion);

            // then
            actualCompletion.Should().BeEquivalentTo(expectedCompletion);

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(It.Is(
                    SameExternalCompletionRequestAs(mappedExternalCompletionRequest))),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
