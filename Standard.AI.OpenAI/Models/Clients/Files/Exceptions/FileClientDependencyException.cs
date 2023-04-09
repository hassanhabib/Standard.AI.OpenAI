// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Files.Exceptions
{
    public class FileClientDependencyException : Xeption
    {
        public FileClientDependencyException(Xeption innerException)
            : base(message: "File client dependency error occurred, contact support.",
                  innerException)
        { }
    }
}