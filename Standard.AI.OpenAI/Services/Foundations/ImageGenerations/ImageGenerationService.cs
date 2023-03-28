// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal class ImageGenerationService : IImageGenerationService
    {
        private readonly IOpenAIBroker openAIBroker;

        public ImageGenerationService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration)
        {
            throw new System.NotImplementedException();
        }
    }
}