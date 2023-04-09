// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Files.Exceptions
{
    public class FileClientServiceException : Xeption
    {
        public FileClientServiceException(Xeption innerException)
            : base(message: "File client service error occurred, contact support.",
                  innerException)
        { }
    }
}