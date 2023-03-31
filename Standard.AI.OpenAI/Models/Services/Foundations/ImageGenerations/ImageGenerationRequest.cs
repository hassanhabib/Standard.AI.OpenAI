// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations
{
    public class ImageGenerationRequest
    {
        public string Prompt { get; set; }
        public int ImagesToGenerate { get; set; }
        public string ImageSize { get; set; }
        public string ResponseFormat { get; set; }
        public string User { get; set; }
    }
}