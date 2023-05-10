// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class AudioTranscriptionDependencyValidationException : Xeption
    {
        public AudioTranscriptionDependencyValidationException(Xeption innerException)
            : base(message: "Chat completion dependency validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}