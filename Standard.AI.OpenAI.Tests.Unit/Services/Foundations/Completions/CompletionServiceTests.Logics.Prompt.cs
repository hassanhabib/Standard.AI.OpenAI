// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Fact]
        public async Task ShouldPromptsCompletionAsync()
        {
            // given
            DateTimeOffset randomDate = GetRandomDate();
            int randomDateNumber = GetRandomNumber();

            dynamic randomCompletionProperties = CreateRandomCompletionProperties(
                createdDate: randomDate,
                createdDateNumber: randomDateNumber);

            var randomCompletionRequest = new CompletionRequest
            {
                Model = randomCompletionProperties.RequestModel,
                Prompts = randomCompletionProperties.Prompts,
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
                CreatedDate = randomCompletionProperties.CreatedDate,
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
                Request = randomCompletionRequest
            };

            var randomExternalCompletionRequest = new ExternalCompletionRequest
            {
                Model = randomCompletionProperties.RequestModel,
                Prompts = randomCompletionProperties.Prompts,
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
                    new ExternalCompletionChoice
                    {
                        FinishReason = item.FinishReason,
                        Index = item.Index,
                        LogProbabilities = item.LogProbabilities,
                        Text = item.Text
                    }).ToArray(),

                Usage = new ExternalCompletionUsage
                {
                    CompletionTokens = randomCompletionProperties.Usage.CompletionTokens,
                    PromptTokens = randomCompletionProperties.Usage.PromptTokens,
                    TotalTokens = randomCompletionProperties.Usage.TotalTokens
                }
            };

            Completion inputCompletion = randomCompletion;
            Completion expectedCompletion = inputCompletion.DeepClone();
            expectedCompletion.Response = randomCompletionResponse;

            ExternalCompletionRequest mappedExternalCompletionRequest =
                randomExternalCompletionRequest;

            ExternalCompletionResponse returnedExternalCompletionResponse =
                randomExternalCompletionResponse;

            this.openAIBrokerMock.Setup(broker =>
                broker.PostCompletionRequestAsync(It.Is(
                    SameExternalCompletionRequestAs(mappedExternalCompletionRequest))))
                        .ReturnsAsync(returnedExternalCompletionResponse);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber))
                    .Returns(randomDate);

            // when
            Completion actualCompletion =
                await this.completionService.PromptCompletionAsync(inputCompletion);

            // then
            actualCompletion.Should().BeEquivalentTo(expectedCompletion);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(It.Is(
                    SameExternalCompletionRequestAs(mappedExternalCompletionRequest))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
