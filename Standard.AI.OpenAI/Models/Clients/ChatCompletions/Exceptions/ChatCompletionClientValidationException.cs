// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class ChatCompletionClientValidationException : Xeption
    {
        public ChatCompletionClientValidationException(Xeption innerException)
            : base(message: "Chat completion client validation error occurred, fix errors and try again.",
                   innerException)
        { }
    }
}
