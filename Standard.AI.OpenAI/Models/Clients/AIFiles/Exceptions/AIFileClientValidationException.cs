// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions
{
    public class AIFileClientValidationException : Xeption
    {
        public AIFileClientValidationException(Xeption innerException)
            : base(message: "AI file client validation error occurred, fix errors and try again.", innerException)
        { }
    }
}
