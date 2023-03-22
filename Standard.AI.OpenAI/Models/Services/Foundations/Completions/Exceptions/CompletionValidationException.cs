// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionValidationException : Xeption
    {
        public CompletionValidationException(Xeption innerException)
            : base(message: "Completion validation error occurred, try again.", innerException)
        { }
    }
}
