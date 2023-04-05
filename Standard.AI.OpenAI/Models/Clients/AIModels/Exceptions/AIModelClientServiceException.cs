// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    /// <summary>
    /// This exception is thrown when a service error occurs while using the AI
    /// model client. For example, if there is a problem with the server or any
    /// other service failure.
    /// </summary>
    public class AIModelClientServiceException : Xeption
    {
        public AIModelClientServiceException(Xeption innerException)
            : base(message: "AI Model client service error occurred, contact support.", innerException)
        { }
    }
}
