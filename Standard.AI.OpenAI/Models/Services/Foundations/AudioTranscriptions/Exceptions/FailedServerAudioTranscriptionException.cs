// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class FailedServerAudioTranscriptionException : Xeption
    {
        public FailedServerAudioTranscriptionException(Exception innerException)
            : base(message: "Failed audio transcription server error occurred, contact support.",
                  innerException)
        { }
    }
}