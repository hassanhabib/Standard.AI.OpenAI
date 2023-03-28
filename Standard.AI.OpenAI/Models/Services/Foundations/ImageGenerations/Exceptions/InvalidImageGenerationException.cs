// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class InvalidImageGenerationException : Xeption
    {
        public InvalidImageGenerationException()
            : base(message: "Image generation is invalid.")
        { }
    }
}