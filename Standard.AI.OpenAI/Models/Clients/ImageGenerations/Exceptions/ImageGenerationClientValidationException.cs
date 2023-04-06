// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    /// <summary>
    /// This exception is thrown when a validation error occurs while using the Image Generation client.
    /// For example, if required data is missing or invalid.
    /// </summary>
    public class ImageGenerationClientValidationException : Xeption
    {
        public ImageGenerationClientValidationException(Xeption innerException)
            : base(message: "Image generation client validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}