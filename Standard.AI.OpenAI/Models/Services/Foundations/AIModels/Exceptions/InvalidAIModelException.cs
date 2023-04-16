// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class InvalidAIModelException : Xeption
    {
        public InvalidAIModelException()
            : base(message: "AI Model is invalid.")
        { }

        public InvalidAIModelException(Exception innerException)
           : base(message: $"AI Model is invalid.", innerException)
        { }
    }
}
