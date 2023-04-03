// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
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
            DateTimeOffset randomDate = GetRandomDate();
            int randomDateNumber = GetRandomNumber();

            dynamic randomChatCompletionProperties = CreateRandomChatCompletionProperties(
                createdDate: randomDate,
                createdDateNumber: randomDateNumber);

            var randomChatCompletionRequest = new ChatCompletionRequest
            {
                Model = randomChatCompletionProperties.Model,

                Messages = ((dynamic[])randomChatCompletionProperties.Messages).Select(message =>
                {
                    return new ChatCompletionMessage
                    {
                        Role = message.Role,
                        Content = message.Content
                    };
                }).ToArray(),

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
                CreatedDate = randomChatCompletionProperties.CreatedDate,

                Choices = ((dynamic[])randomChatCompletionProperties.Choices).Select(choice =>
                {
                    return new ChatCompletionChoice
                    {
                        Index = choice.Index,
                        FinishReason = choice.FinishReason,

                        Message = new ChatCompletionMessage
                        {
                            Role = choice.Message.Role,
                            Content = choice.Message.Content
                        }
                    };
                }).ToArray(),

                Usage = new ChatCompletionUsage
                {
                    CompletionTokens = randomChatCompletionProperties.Usage.CompletionTokens,
                    PromptTokens = randomChatCompletionProperties.Usage.PromptTokens,
                    TotalTokens = randomChatCompletionProperties.Usage.TotalTokens
                }
            };

            var randomChatCompletion = new ChatCompletion
            {
                Request = randomChatCompletionRequest
            };

            var randomExternalChatCompletionRequest = new ExternalChatCompletionRequest
            {
                Model = randomChatCompletionProperties.Model,

                Messages = ((dynamic[])randomChatCompletionProperties.Messages).Select(message =>
                {
                    return new ExternalChatCompletionMessage
                    {
                        Role = message.Role,
                        Content = message.Content
                    };
                }).ToArray(),

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

                Choices = ((dynamic[])randomChatCompletionProperties.Choices).Select(choice =>
                {
                    return new ExternalChatCompletionChoice
                    {
                        Index = choice.Index,
                        FinishReason = choice.FinishReason,

                        Message = new ExternalChatCompletionMessage
                        {
                            Role = choice.Message.Role,
                            Content = choice.Message.Content
                        }
                    };
                }).ToArray(),

                Usage = new ExternalChatCompletionUsage
                {
                    CompletionTokens = randomChatCompletionProperties.Usage.CompletionTokens,
                    PromptTokens = randomChatCompletionProperties.Usage.PromptTokens,
                    TotalTokens = randomChatCompletionProperties.Usage.TotalTokens
                }
            };

            ChatCompletion inputChatCompletion = randomChatCompletion;
            ChatCompletion expectedChatCompletion = inputChatCompletion.DeepClone();
            expectedChatCompletion.Response = randomChatCompletionResponse;

            ExternalChatCompletionRequest mappedExternalChatCompletionRequest =
                randomExternalChatCompletionRequest;

            ExternalChatCompletionResponse returnedExternalChatCompletionResponse =
                randomExternalChatCompletionResponse;

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(It.Is(
                    SameExternalChatCompletionRequestAs(mappedExternalChatCompletionRequest))))
                        .ReturnsAsync(returnedExternalChatCompletionResponse);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber))
                    .Returns(randomDate);

            // when
            ChatCompletion actualChatCompletion =
                await this.chatCompletionService.SendChatCompletionAsync(inputChatCompletion);

            // then
            actualChatCompletion.Should().BeEquivalentTo(expectedChatCompletion);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(It.Is(
                    SameExternalChatCompletionRequestAs(mappedExternalChatCompletionRequest))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(randomDateNumber),
                    Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
