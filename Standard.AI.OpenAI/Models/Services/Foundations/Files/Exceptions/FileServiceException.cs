// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class FileServiceException : Xeption
    {
        public FileServiceException(Xeption innerException)
            : base(message: "File service error occurred, contact support.",
                  innerException)
        { }
    }
}