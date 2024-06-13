// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions
{
    public class AIFileOrchestrationDependencyException : Xeption
    {
        public AIFileOrchestrationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}