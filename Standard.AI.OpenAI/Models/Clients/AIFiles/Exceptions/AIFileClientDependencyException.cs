// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions
{
    public class AIFileClientDependencyException : Xeption
    {
        public AIFileClientDependencyException(Xeption innerException)
            : base(message: "AI file client dependency error occurred, contact support.", innerException)
        { }
    }
}
