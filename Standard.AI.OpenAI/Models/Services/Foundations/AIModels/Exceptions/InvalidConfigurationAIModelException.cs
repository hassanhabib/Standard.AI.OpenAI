// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class InvalidConfigurationAIModelException : Xeption
    {
        public InvalidConfigurationAIModelException(Exception innerException)
            : base(message: "Invalid AI Model configuration error occurred, contact support.", innerException)
        { }
    }
}
