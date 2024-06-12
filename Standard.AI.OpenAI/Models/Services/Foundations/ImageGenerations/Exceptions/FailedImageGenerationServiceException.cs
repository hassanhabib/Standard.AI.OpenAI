// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions
{
    public class FailedImageGenerationServiceException : Xeption
    {
        public FailedImageGenerationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}