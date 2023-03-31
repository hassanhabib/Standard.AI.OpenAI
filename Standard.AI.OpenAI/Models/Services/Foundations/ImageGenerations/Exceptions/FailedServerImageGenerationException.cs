// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class FailedServerImageGenerationException : Xeption
    {
        public FailedServerImageGenerationException(Exception innerException)
            : base(message: "Failed image generation server error occurred, contact support.",
                  innerException)
        { }
    }
}