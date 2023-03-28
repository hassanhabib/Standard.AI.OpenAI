// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class NotFoundImageGenerationException : Xeption
    {
        public NotFoundImageGenerationException(Exception innerException)
            : base(message: "Image generation not found.",
                  innerException)
        { }
    }
}