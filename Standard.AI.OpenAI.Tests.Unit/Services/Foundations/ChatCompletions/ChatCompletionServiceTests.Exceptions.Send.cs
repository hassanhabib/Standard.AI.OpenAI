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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        public async Task ShouldThrowDependencyExceptionOnSendIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var unauthorizedChatCompletionException =
                new UnauthorizedChatCompletionException(unauthorizedException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(unauthorizedChatCompletionException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfChatCompletionNotFoundOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundChatCompletionException =
                new NotFoundChatCompletionException(
                    httpResponseNotFoundException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    notFoundChatCompletionException);

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
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfBadRequestErrorOccurredAsync()
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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfTooManyRequestsOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion =
                CreateRandomChatCompletion();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallChatCompletionException =
                new ExcessiveCallChatCompletionException(
                    httpResponseTooManyRequestsException);

            var expectedChatCompletionDependencyValidationException =
                new ChatCompletionDependencyValidationException(
                    excessiveCallChatCompletionException);

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
        public async Task ShouldThrowDependencyExceptionOnSendIfHttpResponseErrorOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();
            var httpResponseException = new HttpResponseException();

            var failedServerChatCompletionException =
                new FailedServerChatCompletionException(
                    httpResponseException);

            var expectedChatCompletionDependencyException =
                new ChatCompletionDependencyException(
                    failedServerChatCompletionException);

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
        public async Task ShouldThrowServiceExceptionOnSendIfServiceErrorOccurredAsync()
        {
            // given
            ChatCompletion someChatCompletion = CreateRandomChatCompletion();
            var serviceException = new Exception();

            var failedChatCompletionServiceException =
                new FailedChatCompletionServiceException(serviceException);

            var expectedChatCompletionServiceException =
                new ChatCompletionServiceException(
                    failedChatCompletionServiceException);

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
