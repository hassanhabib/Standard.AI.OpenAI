// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class FileValidationException : Xeption
    {
        public FileValidationException(Xeption innerException)
            : base(message: "File validation error occurred, fix error and try again.",
                  innerException)
        { }
    }
}
