// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class ImageGenerationServiceException : Xeption
    {
        public ImageGenerationServiceException(Exception innerException)
            : base(
                message: "Image generation service error occurred, contact support.",
                    innerException: innerException)
        { }

        public ImageGenerationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}