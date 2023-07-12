// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions
{
    /// <summary>
    /// This exception is thrown when a service error occurs while using the File Tune client.
    /// For example, if there is a problem with the server or any other service failure.
    /// </summary>
    public class FineTuneClientServiceException : Xeption
    {
        public FineTuneClientServiceException(Xeption innerException)
            : base(message: "Fine tune client service error occurred, contact support.",
                  innerException)
        { }
    }
}
