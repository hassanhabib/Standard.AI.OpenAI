// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class ChatCompletionClientValidationException : Xeption
    {
        /// <summary>
        /// This exception is thrown when a validation error occurs while using the Chat completion client.
        /// For example, if required data is missing or invalid.
        /// </summary>
        public ChatCompletionClientValidationException(string message, Xeption innerException)
            : base(message, innerException )
        { }
    }
}
