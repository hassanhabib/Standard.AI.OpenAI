// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class InvalidImageGenerationException : Xeption
    {
        public InvalidImageGenerationException()
            : base(message: "Image generation is invalid.")
        { }

        public InvalidImageGenerationException(Exception innerException)
            : base(message: "Image generation is invalid.",
                  innerException)
        { }
    }
}