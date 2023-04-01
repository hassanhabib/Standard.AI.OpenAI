// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class ChatCompletionServiceException : Xeption
    {
        public ChatCompletionServiceException(Exception innerException)
            : base(message: "Chat completion service error occurred, contact support.", innerException)
        { }
    }
}
