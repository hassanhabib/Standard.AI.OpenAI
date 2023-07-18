// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the AI file client.
    /// For example, if a required dependency is unavailable or incompatible.
    /// </summary>
    public class AIFileClientDependencyException : Xeption
    {
        public AIFileClientDependencyException(Xeption innerException)
            : base(message: "AI file client dependency error occurred, contact support.", innerException)
        { }
    }
}
