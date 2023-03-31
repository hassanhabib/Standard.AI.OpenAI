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

            Validate(
                (Rule: IsInvalid(audioTranscription.Request),
                Parameter: nameof(AudioTranscription.Request)));
        }

        private static void ValidateAudioTranscriptionIsNotNull(AudioTranscription audioTranscription)
        {
            if (audioTranscription is null)
            {
                throw new NullAudioTranscriptionException();
            }
        }

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            InvalidAudioTranscriptionException invalidAudioTranscriptionException = new InvalidAudioTranscriptionException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAudioTranscriptionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAudioTranscriptionException.ThrowIfContainsErrors();
        }
    }
}