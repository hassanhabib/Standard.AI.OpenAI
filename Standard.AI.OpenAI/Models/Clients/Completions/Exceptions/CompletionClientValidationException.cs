// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    /// <summary>
    /// This exception is thrown when a validation error occurs while using the completion client.
    /// For example, if required data is missing or invalid.
    /// </summary>
    public class CompletionClientValidationException : Xeption
    {
        public CompletionClientValidationException(Xeption innerException)
            : base(message: "Completion client validation error occurred, fix errors and try again.",
                   innerException)
        { }
    }
}
