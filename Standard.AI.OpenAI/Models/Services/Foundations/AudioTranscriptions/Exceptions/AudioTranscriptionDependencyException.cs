// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class AudioTranscriptionDependencyException : Xeption
    {
        public AudioTranscriptionDependencyException(Xeption innerException)
            : base(
                message: "Audio transcription dependency error occurred, contact support.",
                innerException)
        { }
    }
}