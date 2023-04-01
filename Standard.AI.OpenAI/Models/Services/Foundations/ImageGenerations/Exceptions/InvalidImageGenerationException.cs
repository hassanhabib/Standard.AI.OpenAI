// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class InvalidImageGenerationException : Xeption
    {
        public InvalidImageGenerationException()
            : base(message: "Invalid image generation error occurred, fix errors and try again.")
        { }

        public InvalidImageGenerationException(Exception innerException)
            : base(message: "Invalid image generation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}