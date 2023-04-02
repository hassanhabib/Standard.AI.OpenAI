// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class FailedAudioTranscriptionServiceException : Xeption
    {
        public FailedAudioTranscriptionServiceException(Exception innerException)
            : base(
                message: "Failed Audio Transcription Service Exception occurred, please contact support for assistance.",
                innerException)
        { }
    }
}