// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Clients.Completions.Exceptions
{
    internal class CompletionClientValidationException : Xeption
    {
        public CompletionClientValidationException(Xeption innerException)
            : base(message: "Completion client validation error occurred, fix errors and try again.",
                   innerException)
        { }
    }
}
