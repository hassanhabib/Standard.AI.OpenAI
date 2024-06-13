// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions
{
    public class AIFileOrchestrationDependencyValidationException : Xeption
    {
        public AIFileOrchestrationDependencyValidationException(string message, Xeption innerException)
           : base(message, innerException)
        { }
    }
}