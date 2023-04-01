// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class ChatCompletionValidationException : Xeption
    {
        public ChatCompletionValidationException(Xeption innerException)
            : base(message: "Chat completion validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
