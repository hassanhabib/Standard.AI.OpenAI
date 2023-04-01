// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class ChatCompletionDependencyException : Xeption
    {
        public ChatCompletionDependencyException(Xeption innerException)
            : base(message: "Chat completion dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
