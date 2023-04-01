// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations
{
    public class ImageGenerationResponse
    {
        public DateTimeOffset Created { get; set; }
        public ImageGenerationResult[] Results { get; set; }
    }
}