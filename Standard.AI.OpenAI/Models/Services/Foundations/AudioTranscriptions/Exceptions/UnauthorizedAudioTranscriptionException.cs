// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class UnauthorizedAudioTranscriptionException : Xeption
    {
        public UnauthorizedAudioTranscriptionException(Exception innerException)
            : base(
                message: "Unauthorized audio transcription request, fix errors and try again.",
                innerException)
        { }
    }
}