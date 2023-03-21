// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Services.Foundations.Completions.Exceptions
{
    internal class CompletionServiceException : Xeption
    {
        public CompletionServiceException(Xeption innerException)
            : base(message: "Completion service error occurred, contact support.",
                  innerException)
        { }
    }
}
