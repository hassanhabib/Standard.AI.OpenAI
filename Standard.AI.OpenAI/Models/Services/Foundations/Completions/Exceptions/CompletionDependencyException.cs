// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionDependencyException : Xeption
    {
        public CompletionDependencyException(Xeption innerException)
            : base(message: "Completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
