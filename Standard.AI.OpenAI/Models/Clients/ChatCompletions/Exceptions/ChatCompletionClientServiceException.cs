// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class ChatCompletionClientServiceException : Xeption
    {
        public ChatCompletionClientServiceException(Xeption innerException)
            : base(message: "Chat completion client service error occurred, contact support.",
                  innerException)
        { }
    }
}
