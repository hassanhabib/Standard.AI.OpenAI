// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions
{
    public class AIFileOrchestrationDependencyValidationException : Xeption
    {
        public AIFileOrchestrationDependencyValidationException(Xeption innerException)
            : base(message: "AI file dependency validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
