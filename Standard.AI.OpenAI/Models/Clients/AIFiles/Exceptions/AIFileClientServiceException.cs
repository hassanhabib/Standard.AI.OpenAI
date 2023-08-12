// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions
{
    /// <summary>
    /// This exception is thrown when a service error occurs while using the AI file client.
    /// For example, if there is a problem with the server or any other service failure.
    /// </summary>
    public class AIFileClientServiceException : Xeption
    {
        public AIFileClientServiceException(Xeption innerException)
            : base(
                message: "AI file client service error occurred, contact support.",
                    innerException: innerException)
        { }

        public AIFileClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
