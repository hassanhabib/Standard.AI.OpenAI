// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionDependencyValidationException : Xeption
    {
        public CompletionDependencyValidationException(Exception innerException)
            : base(message: "Completion dependency validation error occurred. Fix errors and try again.", innerException)
        { }
    }
}
