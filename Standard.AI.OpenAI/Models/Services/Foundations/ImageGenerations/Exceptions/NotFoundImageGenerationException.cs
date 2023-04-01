// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class NotFoundImageGenerationException : Xeption
    {
        public NotFoundImageGenerationException(Exception innerException)
            : base(message: "Not found image generation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}