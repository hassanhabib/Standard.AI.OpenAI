// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class ImageGenerationValidationException : Xeption
    {
        public ImageGenerationValidationException(Xeption innerException)
            : base(message: "Image generation validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}