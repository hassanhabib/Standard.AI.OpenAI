// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Clients.ImageGenerations
{
    public interface IImageGenerationsClient
    {
        /// <exception cref="ImageGenerationClientValidationException" />
        /// <exception cref="ImageGenerationClientDependencyException" />
        /// <exception cref="ImageGenerationClientServiceException" />
        ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration);
    }
}