// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class FailedAIModelServiceException : Xeption
    {
        public FailedAIModelServiceException(Exception innerException)
            : base(message: "Failed AI Model Service Exception occurred, please contact support for assistance.",
                innerException)
        { }
    }
}