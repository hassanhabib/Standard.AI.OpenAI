// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;

namespace Standard.AI.OpenAI.Clients.ImageGenerations
{
    public interface IImageGenerationsClient
    {
        ValueTask<ImageGeneration> GenerateImageAsync(ImageGeneration imageGeneration);
    }
}