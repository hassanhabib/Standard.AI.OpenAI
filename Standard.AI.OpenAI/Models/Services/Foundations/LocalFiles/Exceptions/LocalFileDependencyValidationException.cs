// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class LocalFileDependencyValidationException : Xeption
    {
        public LocalFileDependencyValidationException(Xeption innerException)
            : base(message: "Local file dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
