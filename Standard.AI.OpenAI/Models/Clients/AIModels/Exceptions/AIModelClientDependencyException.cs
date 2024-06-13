// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the
    /// AI model client. For example, if a required dependency is unavailable or
    /// incompatible.
    /// </summary>
    public class AIModelClientDependencyException : Xeption
    {
        public AIModelClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
