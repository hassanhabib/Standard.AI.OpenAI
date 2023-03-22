// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionServiceException : Xeption
    {
        public CompletionServiceException(Exception innerException)
            : base(message: "Completion service error occurred contact support.", innerException)
        { }
    }
}
