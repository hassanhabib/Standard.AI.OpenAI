// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ChatCompletions
{
    public partial class ChatCompletionServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSendIfUrlNotFoundAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationChatCompletionException =
                new InvalidConfigurationChatCompletionException(
                    httpResponseUrlNotFoundException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(
                    invalidConfigurationChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(someChatCompletion);

            ChatCompletionDependencyException
                actualChatCompletionDependencyException =
                    await Assert.ThrowsAsync<ChatCompletionDependencyException>(
                        sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionDependencyException.Should().BeEquivalentTo(
                expectedChatCompletionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfDependencyValidationErrorOccursAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidChatCompletionException =
                new InvalidChatCompletionException(
                    httpResponseBadRequestException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    invalidChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(someChatCompletion);

            ChatCompletionDependencyValidationException
                actualChatCompletionDependencyValidationException =
                    await Assert.ThrowsAsync<ChatCompletionDependencyValidationException>(
                        sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionDependencyValidationException.Should().BeEquivalentTo(
                expectedChatCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
