// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionValidationException : Xeption
    {
        public CompletionValidationException(Xeption innerException)
            : base(message: "Completion validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
