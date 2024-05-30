// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions
{
    public class ChatCompletionDependencyException : Xeption
    {
        public ChatCompletionDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
