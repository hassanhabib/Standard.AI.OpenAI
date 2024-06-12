// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the Chat completion client.
    /// For example, if a required dependency is unavailable or incompatible.
    /// </summary>
    public class ChatCompletionClientDependencyException : Xeption
    {
        public ChatCompletionClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
