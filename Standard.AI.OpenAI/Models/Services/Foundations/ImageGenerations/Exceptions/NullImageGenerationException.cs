// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class NullImageGenerationException : Xeption
    {
        public NullImageGenerationException()
            : base(message: "Image generation is null.")
        { }
    }
}