// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class InvalidConfigurationAudioTranscriptionException : Xeption
    {
        public InvalidConfigurationAudioTranscriptionException(Exception innerException)
            : base(
                message: "Invalid audio transcription configuration error occurred, contact support.",
                innerException: innerException)
        { }
    }
}