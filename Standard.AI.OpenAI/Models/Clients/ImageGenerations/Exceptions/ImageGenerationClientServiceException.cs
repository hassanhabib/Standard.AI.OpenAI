// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    /// <summary>
    /// This exception is thrown when a service error occurs while using the Image Generation client. 
    /// For example, if there is a problem with the server or any other service failure.
    /// </summary>
    public class ImageGenerationClientServiceException : Xeption
    {
        public ImageGenerationClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}