// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionValidationException : Xeption
    {
        public CompletionValidationException(Exception innerException)
            : base(message: "Completion validation error occurred, try again.", innerException)
        { }
    }
}
