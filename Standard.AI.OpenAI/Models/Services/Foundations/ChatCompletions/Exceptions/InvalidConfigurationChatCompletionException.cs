// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class InvalidConfigurationChatCompletionException : Xeption
    {
        public InvalidConfigurationChatCompletionException(Exception innerException)
            : base(message: "Invalid chat completion configuration error occurred, contact support.", innerException)
        { }
    }
}
