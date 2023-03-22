// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    internal class CompletionClientDependencyException : Xeption
    {
        public CompletionClientDependencyException(Xeption innerException)
            : base(message: "Completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
