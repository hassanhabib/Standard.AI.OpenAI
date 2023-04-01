// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class FailedImageGenerationServiceException : Xeption
    {
        public FailedImageGenerationServiceException(Exception innerException)
            : base(message: "Failed image generation service error occurred, contact support.",
                  innerException)
        { }
    }
}