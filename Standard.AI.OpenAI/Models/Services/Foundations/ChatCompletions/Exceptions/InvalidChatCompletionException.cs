// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class InvalidChatCompletionException : Xeption
    {
        public InvalidChatCompletionException(string message)
            : base(
                message: "Chat completion is invalid.")
        { }
        public InvalidChatCompletionException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
