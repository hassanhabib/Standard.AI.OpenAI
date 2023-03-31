// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class NullAudioTranscriptionException : Xeption
    {
        public NullAudioTranscriptionException()
            : base(message: "Audio transcription is null.")
        { }
    }
}