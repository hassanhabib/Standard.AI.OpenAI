// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
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
        public async Task ShouldThrowValidationExceptionOnSendIfAudioTranscriptionIsNullAsync()
        {
            // given
            AudioTranscription audioTranscription = null;

            NullAudioTranscriptionException nullAudioTranscriptionException = new();

            AudioTranscriptionValidationException exceptedAudioTranscriptionValidationException =
                new(nullAudioTranscriptionException);

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
