// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions
{
    internal class NullAIFileOrchestrationException : Xeption
    {
        public NullAIFileOrchestrationException(string message)
            : base(message)
        { }

        public NullAIFileOrchestrationException(string message, Xeption innerException)
           : base(message, innerException)
        { }
    }
}