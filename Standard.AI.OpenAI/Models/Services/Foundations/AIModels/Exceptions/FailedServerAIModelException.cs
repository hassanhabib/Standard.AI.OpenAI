// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class FailedServerAIModelException : Xeption
    {
        public FailedServerAIModelException(Exception innerException)
            : base(message: "Failed AI Model server error occurred, contact support",
                  innerException)
        { }
    }
}
