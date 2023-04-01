// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal interface IImageGenerationService
    {
        ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration);
    }
}