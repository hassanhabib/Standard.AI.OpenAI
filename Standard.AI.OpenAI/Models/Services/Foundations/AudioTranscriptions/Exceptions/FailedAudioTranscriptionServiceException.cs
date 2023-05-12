// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class FailedAudioTranscriptionServiceException : Xeption
    {
        public FailedAudioTranscriptionServiceException(Exception innerException)
            : base(
                message: "Failed Audio Transcription Service Exception occurred, please contact support for assistance.",
                innerException)
        { }
    }
}