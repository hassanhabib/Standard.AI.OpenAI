// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal partial class ImageGenerationService
    {
        private static void ValidateImageGenerationIsNotNull(ImageGeneration imageGeneration)
        {
            if (imageGeneration is null)
            {
                throw new NullImageGenerationException();
            }
        }
    }
}