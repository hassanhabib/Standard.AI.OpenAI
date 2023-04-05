// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    public class AIModelClientDependencyException : Xeption
    {
        public AIModelClientDependencyException(Xeption innerException)
            : base(message: "AI model client validation error occurred, fix the errors and try again", innerException)
        { }
    }
}
