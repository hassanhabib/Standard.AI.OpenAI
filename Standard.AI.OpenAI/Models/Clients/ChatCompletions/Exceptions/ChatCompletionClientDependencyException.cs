// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class ChatCompletionClientDependencyException : Xeption
    {
        public ChatCompletionClientDependencyException(Xeption innerException)
            : base(message: "Chat completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
