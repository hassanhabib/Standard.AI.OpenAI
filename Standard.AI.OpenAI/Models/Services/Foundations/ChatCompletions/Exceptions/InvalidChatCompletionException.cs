// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class InvalidChatCompletionException : Xeption
    {
        public InvalidChatCompletionException()
            : base(message: "Chat completion is invalid.")
        { }

        public InvalidChatCompletionException(Exception innerException)
            : base(message: $"Chat completion is invalid.",
                  innerException)
        { }
    }
}
