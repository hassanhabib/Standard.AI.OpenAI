// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class InvalidConfigurationChatCompletionException : Xeption
    {
        public InvalidConfigurationChatCompletionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}