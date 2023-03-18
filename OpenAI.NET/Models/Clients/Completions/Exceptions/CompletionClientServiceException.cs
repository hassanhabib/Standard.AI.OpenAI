// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Clients.Completions.Exceptions
{
    internal class CompletionClientServiceException : Xeption
    {
        public CompletionClientServiceException(Xeption innerException)
            : base(message: "Completion client service error occurred, contact support.",
                  innerException)
        { }
    }
}
