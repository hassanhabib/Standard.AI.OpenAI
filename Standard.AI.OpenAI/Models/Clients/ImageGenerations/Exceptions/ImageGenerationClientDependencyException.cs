// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the Image Generation client. 
    /// For example, if a required dependency is unavailable or incompatible.
    /// </summary>
    public class ImageGenerationClientDependencyException : Xeption
    {
        public ImageGenerationClientDependencyException(Xeption innerException)
            : base(message: "Image generation client dependency error occurred, contact support.",
                  innerException)
        { }
    }
}