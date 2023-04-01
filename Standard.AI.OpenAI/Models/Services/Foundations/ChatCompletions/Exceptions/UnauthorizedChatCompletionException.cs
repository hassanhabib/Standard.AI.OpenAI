// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class UnauthorizedChatCompletionException : Xeption
    {
        public UnauthorizedChatCompletionException(Exception innerException)
            : base(message: "Unauthorized chat completion request, fix errors and try again.", innerException)
        { }
    }
}
