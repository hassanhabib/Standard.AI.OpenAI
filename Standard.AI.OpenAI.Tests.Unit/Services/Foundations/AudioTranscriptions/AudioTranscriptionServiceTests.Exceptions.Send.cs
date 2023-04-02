// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

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

            using AutoRemovableFile _ = new(someAudioTranscription.Request.FilePath);

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

            using AutoRemovableFile _ = new(someAudioTranscription.Request.FilePath);

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
        public async Task ShouldThrowDependencyValidationExceptionOnSendIfAudioTranscriptionNotFoundOccurredAsync()
        {
            // given
            AudioTranscription someAudioTranscription =
                CreateRandomAudioTranscription();

            using var _ = new AutoRemovableFile(someAudioTranscription.Request.FilePath);

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundAudioTranscriptionException =
                new NotFoundAudioTranscriptionException(
                    httpResponseNotFoundException);

            var expectedAudioTranscriptionDependencyValidationException =
                new AudioTranscriptionDependencyValidationException(
                    notFoundAudioTranscriptionException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()))
                        .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<AudioTranscription> promptAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(someAudioTranscription);

            AudioTranscriptionDependencyValidationException actualAudioTranscriptionDependencyException =
                await Assert.ThrowsAsync<AudioTranscriptionDependencyValidationException>(
                    promptAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyValidationException);

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

            using AutoRemovableFile _ = new(someAudioTranscription.Request.FilePath);

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

            AudioTranscriptionDependencyValidationException actualAudioTranscriptionDependencyException =
                await Assert.ThrowsAsync<AudioTranscriptionDependencyValidationException>(
                    promptAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionDependencyException.Should().BeEquivalentTo(
                expectedAudioTranscriptionDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
