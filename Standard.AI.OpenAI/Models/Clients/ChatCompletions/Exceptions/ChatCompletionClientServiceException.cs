// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions
{
    public class ChatCompletionClientServiceException : Xeption
    {
        /// <summary>
        /// This exception is thrown when a service error occurs while using the Chat completion client.
        /// For example, if there is a problem with the server or any other service failure.
        /// </summary>
        public ChatCompletionClientServiceException(Xeption innerException)
            : base(message: "Chat completion client service error occurred, contact support.",
                  innerException)
        { }
    }
}
