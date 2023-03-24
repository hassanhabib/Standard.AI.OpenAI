// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ChatCompletions
{
    public partial class ChatCompletionServiceTests
    {
        [Fact]
        public async Task ShouldSendChatCompletionAsync()
        {
            // given
            dynamic randomChatCompletionProperties = CreateRandomChatCompletionProperties();

            var randomChatCompletionRequest = new ChatCompletionRequest
            {
                Model = randomChatCompletionProperties.Model,
                Messages = randomChatCompletionProperties.Messages,
                Temperature = randomChatCompletionProperties.Temperature,
                ProbabilityMass = randomChatCompletionProperties.ProbabilityMass,
                CompletionsPerPrompt = randomChatCompletionProperties.CompletionsPerPrompt,
                Stream = randomChatCompletionProperties.Stream,
                Stop = randomChatCompletionProperties.Stop,
                MaxTokens = randomChatCompletionProperties.MaxTokens,
                PresencePenalty = randomChatCompletionProperties.PresencePenalty,
                FrequencyPenalty = randomChatCompletionProperties.FrequencyPenalty,
                LogitBias = randomChatCompletionProperties.LogitBias,
                User = randomChatCompletionProperties.User
            };

            var randomChatCompletionResponse = new ChatCompletionResponse
            {
                Id = randomChatCompletionProperties.Id,
                Object = randomChatCompletionProperties.Object,
                Created = randomChatCompletionProperties.Created,
                Choices = randomChatCompletionProperties.Choices,
                Usage = randomChatCompletionProperties.Usage
            };

            var randomChatCompletion = new ChatCompletion
            {
                Request = randomChatCompletionRequest
            };

            var randomExternalChatCompletionRequest = new ExternalChatCompletionRequest
            {
                Model = randomChatCompletionProperties.Model,
                Messages = randomChatCompletionProperties.Messages,
                Temperature = randomChatCompletionProperties.Temperature,
                ProbabilityMass = randomChatCompletionProperties.ProbabilityMass,
                CompletionsPerPrompt = randomChatCompletionProperties.CompletionsPerPrompt,
                Stream = randomChatCompletionProperties.Stream,
                Stop = randomChatCompletionProperties.Stop,
                MaxTokens = randomChatCompletionProperties.MaxTokens,
                PresencePenalty = randomChatCompletionProperties.PresencePenalty,
                FrequencyPenalty = randomChatCompletionProperties.FrequencyPenalty,
                LogitBias = randomChatCompletionProperties.LogitBias,
                User = randomChatCompletionProperties.User
            };

            var randomExternalChatCompletionResponse = new ExternalChatCompletionResponse
            {
                Id = randomChatCompletionProperties.Id,
                Object = randomChatCompletionProperties.Object,
                Created = randomChatCompletionProperties.Created,
                Choices = randomChatCompletionProperties.Choices,
                Usage = randomChatCompletionProperties.Usage
            };

            ChatCompletion inputChatCompletion = randomChatCompletion;
            ChatCompletion expectedChatCompletion = inputChatCompletion.DeepClone();
            expectedChatCompletion.Response = randomChatCompletionResponse;

            ExternalChatCompletionRequest mappedExternalChatCompletionRequest =
                randomExternalChatCompletionRequest;

            ExternalChatCompletionResponse returnedExternalChatCompletionResponse =
                randomExternalChatCompletionResponse;

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionAsync(It.Is(
                    SameExternalChatCompletionRequestAs(mappedExternalChatCompletionRequest))))
                        .ReturnsAsync(returnedExternalChatCompletionResponse);

            // when
            ChatCompletion actualChatCompletion =
                await this.chatCompletionService.SendChatCompletionAsync(inputChatCompletion);

            // then
            actualChatCompletion.Should().BeEquivalentTo(expectedChatCompletion);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionAsync(It.Is(
                    SameExternalChatCompletionRequestAs(mappedExternalChatCompletionRequest))),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
