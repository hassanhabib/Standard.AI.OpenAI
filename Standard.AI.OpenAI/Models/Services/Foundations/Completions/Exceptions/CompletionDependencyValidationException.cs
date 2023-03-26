// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionDependencyValidationException : Xeption
    {
        public CompletionDependencyValidationException(Xeption innerException)
            : base(message: "Completion dependency validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}