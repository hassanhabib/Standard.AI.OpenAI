// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class InvalidChatCompletionException : Xeption
    {
        public InvalidChatCompletionException()
            : base(message: "Chat completion is invalid.")
        { }
    }
}
