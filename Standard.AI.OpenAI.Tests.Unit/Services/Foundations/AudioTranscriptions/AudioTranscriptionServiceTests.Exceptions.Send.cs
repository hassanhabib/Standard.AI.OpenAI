// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AudioTranscriptions
{
    public partial class AudioTranscriptionServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSendIfUrlNotFoundAsync()
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAudioTranscriptionException =
                new InvalidConfigurationAudioTranscriptionException(
                    httpResponseUrlNotFoundException);

            var expectedAudioTranscriptionDependencyException =
                new AudioTranscriptionDependencyException(
                    invalidConfigurationAudioTranscriptionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionDependencyException
                actualAudioTranscriptionDependencyException =
                    await Assert.ThrowsAsync<AudioTranscriptionDependencyException>(
                        sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnAuthorizationExceptions))]
        public async Task ShouldThrowDependencyExceptionOnSendIfUnAuthorizedAsync(
            HttpResponseException unAuthorizationException)
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            var unauthorizedAudioTranscriptionException =
                new UnauthorizedAudioTranscriptionException(unAuthorizationException);

            var expectedAudioTranscriptionDependencyException =
                new AudioTranscriptionDependencyException(unauthorizedAudioTranscriptionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(unAuthorizationException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionDependencyException
                actualAudioTranscriptionDependencyException =
                    await Assert.ThrowsAsync<AudioTranscriptionDependencyException>(
                        sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnUploadIfBadRequestErrorOccurredAsync()
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidAudioTranscriptionException =
                new InvalidAudioTranscriptionException(
                    httpResponseBadRequestException);

            var expectedAudioTranscriptionDependencyValidationException =
                new AudioTranscriptionDependencyValidationException(
                    invalidAudioTranscriptionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionDependencyValidationException actualAudioTranscriptionDependencyValidationException =
                await Assert.ThrowsAsync<AudioTranscriptionDependencyValidationException>(
                    sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyValidationException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
               broker.PostAudioTranscriptionRequestAsync(It.IsAny<ExternalAudioTranscriptionRequest>()),
                   Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSendIfServiceErrorOccurredAsync()
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            var serviceException = new Exception();

            var failedAudioTranscriptionServiceException =
                new FailedAudioTranscriptionServiceException(serviceException);

            var expectedAudioTranscriptionServiceException =
                new AudioTranscriptionServiceException(
                    failedAudioTranscriptionServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(serviceException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionServiceException
                actualAudioTranscriptionServiceException =
                    await Assert.ThrowsAsync<AudioTranscriptionServiceException>(
                        sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionServiceException.Should().BeEquivalentTo(
                expectedAudioTranscriptionServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfTooManyRequestsOccurredAsync()
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallAudioTranscriptionException =
                new ExcessiveCallAudioTranscriptionException(
                    httpResponseTooManyRequestsException);

            var expectedAudioTranscriptionDependencyValidationException =
                new AudioTranscriptionDependencyValidationException(
                    excessiveCallAudioTranscriptionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<AudioTranscription> promptAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionDependencyValidationException actualAudioTranscriptionDependencyValidationException =
                await Assert.ThrowsAsync<AudioTranscriptionDependencyValidationException>(
                    promptAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyValidationException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
