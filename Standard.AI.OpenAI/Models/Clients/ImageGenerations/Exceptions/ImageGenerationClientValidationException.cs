// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    public class ImageGenerationClientValidationException : Xeption
    {
        public ImageGenerationClientValidationException(Xeption innerException)
            : base(message: "Image generation client validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}