// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    public class AIModelClientValidationException : Xeption
    {
        public AIModelClientValidationException(Xeption innerException)
            : base(message: "AI model client dependency error occurred, contact support.", innerException)
        { }
    }
}