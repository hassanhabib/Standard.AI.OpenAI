// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class NullAudioTranscriptionException : Xeption
    {
        public NullAudioTranscriptionException()
            : base(message: "Audio transcription is null.")
        { }
    }
}