// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    internal class AIModelDependencyException : Xeption
    {
        public AIModelDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
