// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class InvalidAudioTranscriptionException : Xeption
    {
        public InvalidAudioTranscriptionException(string message)
            : base(message)
        { }

        public InvalidAudioTranscriptionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}