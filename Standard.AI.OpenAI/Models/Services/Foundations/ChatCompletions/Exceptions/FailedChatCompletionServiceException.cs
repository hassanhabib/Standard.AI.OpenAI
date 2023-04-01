// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class FailedChatCompletionServiceException : Xeption
    {
        public FailedChatCompletionServiceException(Exception innerException)
                : base(message: "Failed Chat Completion Service Exception occurred, please contact support for assistance.",
                      innerException)
        { }
    }
}
