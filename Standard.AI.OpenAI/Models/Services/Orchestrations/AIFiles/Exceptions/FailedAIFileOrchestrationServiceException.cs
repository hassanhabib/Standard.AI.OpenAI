// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions
{
    public class FailedAIFileOrchestrationServiceException : Xeption
    {
        public FailedAIFileOrchestrationServiceException(string message, Exception innerException)
           : base(message, innerException)
        { }
    }
}