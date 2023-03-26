// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    internal class ExcessiveCallChatCompletionException : Xeption
    {
        public ExcessiveCallChatCompletionException(Exception innerException)
            : base(message: "Excessive call error occurred, limit your calls.", innerException)
        { }
    }
}
