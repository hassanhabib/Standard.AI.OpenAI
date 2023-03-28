// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations
{
    public class ImageGenerationResponse
    {
        public int Created { get; set; }
        public ImageGenerationResult[] Results { get; set; }
    }
}