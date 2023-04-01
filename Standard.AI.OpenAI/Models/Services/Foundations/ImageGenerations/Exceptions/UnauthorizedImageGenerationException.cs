// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class UnauthorizedImageGenerationException : Xeption
    {
        public UnauthorizedImageGenerationException(Exception innerException)
            : base(message: "Unauthorized image generation request, fix errors and try again.",
                  innerException)
        { }
    }
}