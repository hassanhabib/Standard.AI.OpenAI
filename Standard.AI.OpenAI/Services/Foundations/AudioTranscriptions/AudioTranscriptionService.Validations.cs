// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions
{
    internal partial class AudioTranscriptionService : IAudioTranscriptionService
    {
        private static void ValidateAudioTranscriptionOnSend(AudioTranscription audioTranscription)
        {
            ValidateAudioTranscriptionIsNotNull(audioTranscription);
        }

        private static void ValidateAudioTranscriptionIsNotNull(AudioTranscription audioTranscription)
        {
            if (audioTranscription is null)
            {
                throw new NullAudioTranscriptionException();
            }
        }
    }
}