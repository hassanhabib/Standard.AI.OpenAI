// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class NullChatCompletionException : Xeption
    {
        public NullChatCompletionException()
            : base(message: "Chat completion is null.")
        { }
    }
}
