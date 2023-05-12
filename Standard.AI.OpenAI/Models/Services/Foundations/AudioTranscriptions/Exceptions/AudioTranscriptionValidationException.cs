// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class AudioTranscriptionValidationException : Xeption
    {
        public AudioTranscriptionValidationException(Xeption innerException)
            : base(message: "Audio transcription validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}