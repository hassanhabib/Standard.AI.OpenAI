// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Completions.Exceptions
{
    internal class CompletionDependencyException : Xeption
    {
        public CompletionDependencyException(Xeption innerException)
            : base(message: "Completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
