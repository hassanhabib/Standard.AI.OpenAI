// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    public class AIModelClientDependencyValidationException : Xeption
    {
        public AIModelClientDependencyValidationException(Xeption innerException)
            : base(message: "AI model client validation error occurred, fix the errors and try again", innerException)
        { }
    }
}
