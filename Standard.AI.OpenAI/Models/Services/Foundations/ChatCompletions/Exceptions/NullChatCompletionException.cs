// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class NullChatCompletionException : Xeption
    {
        public NullChatCompletionException()
            : base(
                message: "Chat completion is null.")
        { }

        public NullChatCompletionException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}