// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    /// <summary>
    /// This exception is thrown when a service error occurs while using the completion client. 
    /// For example, if there is a problem with the server or any other service failure.
    /// </summary>
    public class CompletionClientServiceException : Xeption
    {
        public CompletionClientServiceException(Xeption innerException)
            : base(message: "Completion client service error occurred, contact support.",
                  innerException)
        { }
    }
}
