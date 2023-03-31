// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class InvalidConfigurationImageGenerationException : Xeption
    {
        public InvalidConfigurationImageGenerationException(Exception innerException)
            : base(message: "Invalid image generation configuration error occurred, contact support.",
                  innerException)
        { }
    }
}