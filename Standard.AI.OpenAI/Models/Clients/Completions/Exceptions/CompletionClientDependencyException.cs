// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the completion client. 
    /// For example, if a required dependency is unavailable or incompatible.
    /// </summary>
    public class CompletionClientDependencyException : Xeption
    {
        public CompletionClientDependencyException(string message, Xeption innerException)
           : base(message, innerException)
        { }
    }
}
