// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class CompletionClientValidationException : Xeption
    {
        public CompletionClientValidationException(Xeption innerException)
            : base(message: "Completion client validation error occurred, fix errors and try again.",
                   innerException)
        { }
    }
}
