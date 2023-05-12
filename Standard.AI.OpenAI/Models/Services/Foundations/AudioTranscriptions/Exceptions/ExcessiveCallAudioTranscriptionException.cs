// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions
{
    public class ExcessiveCallAudioTranscriptionException : Xeption
    {
        public ExcessiveCallAudioTranscriptionException(Exception innerException)
            : base(message: "Excessive call error occurred, limit your calls.", innerException)
        { }
    }
}