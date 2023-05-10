// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class AudioTranscriptionServiceException : Xeption
    {
        public AudioTranscriptionServiceException(Exception innerException)
            : base(
                message: "Audio transcription service error occurred, contact support.",
                innerException)
        { }
    }
}