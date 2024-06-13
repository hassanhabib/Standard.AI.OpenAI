// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions
{
    /// <summary>
    /// This exception is thrown when a dependency error occurs while using the File Tune client.
    /// For example, if a required dependency is unavailable or incompatible.
    /// </summary>
    public class FineTuneClientDependencyException : Xeption
    {
        public FineTuneClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
