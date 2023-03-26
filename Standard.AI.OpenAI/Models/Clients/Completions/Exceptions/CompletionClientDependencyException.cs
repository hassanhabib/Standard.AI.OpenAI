// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    public class CompletionClientDependencyException : Xeption
    {
        public CompletionClientDependencyException(Xeption innerException)
            : base(message: "Completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
