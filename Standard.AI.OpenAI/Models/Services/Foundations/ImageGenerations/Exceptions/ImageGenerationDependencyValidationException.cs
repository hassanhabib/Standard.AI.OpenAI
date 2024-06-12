// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class ImageGenerationDependencyValidationException : Xeption
    {
        public ImageGenerationDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}