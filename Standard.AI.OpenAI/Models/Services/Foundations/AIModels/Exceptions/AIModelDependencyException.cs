// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    internal class AIModelDependencyException : Xeption
    {
        public AIModelDependencyException(Xeption innerException)
            : base(message: "AI Model dependency error occurred, contact support.",
                innerException)
        { }
    }
}
