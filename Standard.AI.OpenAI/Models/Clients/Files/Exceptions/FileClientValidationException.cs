// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Files.Exceptions
{
    public class FileClientValidationException : Xeption
    {
        public FileClientValidationException(Xeption innerException)
            : base(message: "File client validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}