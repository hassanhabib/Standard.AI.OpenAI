// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class NotFoundChatCompletionException : Xeption
    {
        public NotFoundChatCompletionException(Exception innerException)
            : base(message: "Chat completion not found.", innerException)
        { }
    }
}
