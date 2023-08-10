// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        private async Task ShouldThrowDependencyExceptionOnSendIfUrlNotFoundAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationChatCompletionException =
                new InvalidConfigurationChatCompletionException(
                    message: "Invalid chat completion configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(
                    message: "Chat completion dependency error occurred, contact support.",
                        innerException: invalidConfigurationChatCompletionException);

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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnSendIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var unauthorizedChatCompletionException =
                new UnauthorizedChatCompletionException(
                    message: "Unauthorized chat completion request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(
                    message: "Chat completion dependency error occurred, contact support.",
                        innerException: unauthorizedChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(unauthorizedException);

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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnSendIfChatCompletionNotFoundOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundChatCompletionException =
                new NotFoundChatCompletionException(
                    message: "Chat completion not found.",
                        innerException: httpResponseNotFoundException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    message: "Chat completion dependency validation error occurred, fix errors and try again.",
                        innerException: notFoundChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<ChatCompletion> promptChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(someChatCompletion);

            ChatCompletionDependencyValidationException actualChatCompletionDependencyException =
                await Assert.ThrowsAsync<ChatCompletionDependencyValidationException>(
                    promptChatCompletionTask.AsTask);

            // then
            actualChatCompletionDependencyException.Should().BeEquivalentTo(
                expectedChatCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnSendIfBadRequestErrorOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidChatCompletionException =
                new InvalidChatCompletionException(
                    message: "Chat completion is invalid.",
                        innerException: httpResponseBadRequestException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    message: "Chat completion dependency validation error occurred, fix errors and try again.",
                        innerException: invalidChatCompletionException);

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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnSendIfTooManyRequestsOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallChatCompletionException =
                new ExcessiveCallChatCompletionException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    message: "Chat completion dependency validation error occurred, fix errors and try again.",
                        innerException: excessiveCallChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<ChatCompletion> promptChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(someChatCompletion);

            ChatCompletionDependencyValidationException actualChatCompletionDependencyException =
                await Assert.ThrowsAsync<ChatCompletionDependencyValidationException>(
                    promptChatCompletionTask.AsTask);

            // then
            actualChatCompletionDependencyException.Should().BeEquivalentTo(
                expectedChatCompletionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnSendIfHttpResponseErrorOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();
            var httpResponseException = new HttpResponseException();

            var failedServerChatCompletionException =
                new FailedServerChatCompletionException(
                    message: "Failed chat completion server error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(
                    message: "Chat completion dependency error occurred, contact support.",
                        innerException: failedServerChatCompletionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(httpResponseException);

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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnSendIfServiceErrorOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();
            var serviceException = new Exception();

            var failedChatCompletionServiceException =
                new FailedChatCompletionServiceException(
                    message: "Failed Chat Completion Service Exception occurred, please contact support for assistance.",
                        innerException: serviceException);

            var expectedChatCompletionServiceException =
                new ChatCompletionServiceException(
                    message: "Chat completion service error occurred, contact support.",
                        innerException: failedChatCompletionServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()))
                        .ThrowsAsync(serviceException);

            // when
            ValueTask<ChatCompletion> sendChatCompletionTask =
                this.chatCompletionService.SendChatCompletionAsync(someChatCompletion);

            ChatCompletionServiceException
                actualChatCompletionServiceException =
                    await Assert.ThrowsAsync<ChatCompletionServiceException>(
                        sendChatCompletionTask.AsTask);

            // then
            actualChatCompletionServiceException.Should().BeEquivalentTo(
                expectedChatCompletionServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostChatCompletionRequestAsync(
                    It.IsAny<ExternalChatCompletionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
