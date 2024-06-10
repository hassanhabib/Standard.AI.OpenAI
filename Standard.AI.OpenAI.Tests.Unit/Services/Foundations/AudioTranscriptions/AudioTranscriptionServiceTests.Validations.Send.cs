// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AudioTranscriptions
{
    public partial class AudioTranscriptionServiceTests
    {
        [Fact]
        private async Task ShouldThrowValidationExceptionOnSendIfAudioTranscriptionIsNullAsync()
        {
            // given
            AudioTranscription audioTranscription = null;

            var nullAudioTranscriptionException = new NullAudioTranscriptionException();

            var exceptedAudioTranscriptionValidationException =
                new AudioTranscriptionValidationException(
                    message: "Audio transcription validation error occurred, fix errors and try again.",
                        innerException: nullAudioTranscriptionException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(audioTranscription);

            AudioTranscriptionValidationException actualAudioTranscriptionValidationException =
                await Assert.ThrowsAsync<AudioTranscriptionValidationException>(
                    sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionValidationException.Should()
                .BeEquivalentTo(exceptedAudioTranscriptionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowValidationExceptionOnSendIfAudioTranscriptionRequestIsNullAsync()
        {
            // given
            var audioTranscription = new AudioTranscription()
            {
                Request = null
            };

            var invalidAudioTranscriptionException =
                createInvalidAudioTranscriptionException();

            invalidAudioTranscriptionException.AddData(
                key: nameof(AudioTranscription.Request),
                values: "Value is required");

            var exceptedAudioTranscriptionValidationException =
                new AudioTranscriptionValidationException(
                    message: "Audio transcription validation error occurred, fix errors and try again.",
                        innerException: invalidAudioTranscriptionException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(audioTranscription);

            AudioTranscriptionValidationException actualAudioTranscriptionValidationException =
                await Assert.ThrowsAsync<AudioTranscriptionValidationException>(
                    sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionValidationException.Should()
                .BeEquivalentTo(exceptedAudioTranscriptionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnSendIfAudioTranscriptionRequestIsInvalidAsync(string invalidText)
        {
            // given
            var audioTranscription = new AudioTranscription()
            {
                Request = new AudioTranscriptionRequest
                {
                    FileName = invalidText,
                    Model = default
                }
            };

            var invalidAudioTranscriptionException =
                createInvalidAudioTranscriptionException();

            invalidAudioTranscriptionException.AddData(
                key: nameof(AudioTranscriptionRequest.FileName),
                values: "Value is required");

            invalidAudioTranscriptionException.AddData(
                key: nameof(AudioTranscriptionRequest.Model),
                values: "Value is required");

            var exceptedAudioTranscriptionValidationException =
                new AudioTranscriptionValidationException(
                    message: "Audio transcription validation error occurred, fix errors and try again.",
                        innerException: invalidAudioTranscriptionException);

            // when
            ValueTask<AudioTranscription> sendAudioTranscriptionTask =
                this.audioTranscriptionService.SendAudioTranscriptionAsync(audioTranscription);

            AudioTranscriptionValidationException actualAudioTranscriptionValidationException =
                await Assert.ThrowsAsync<AudioTranscriptionValidationException>(
                    sendAudioTranscriptionTask.AsTask);

            // then
            actualAudioTranscriptionValidationException.Should()
                .BeEquivalentTo(exceptedAudioTranscriptionValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostAudioTranscriptionRequestAsync(
                    It.IsAny<ExternalAudioTranscriptionRequest>()),
                        Times.Never);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}