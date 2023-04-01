// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class ImageGenerationDependencyException : Xeption
    {
        public ImageGenerationDependencyException(Xeption innerException)
            : base(message: "Image generation dependency error occurred, contact support.",
                  innerException)
        { }
    }
}