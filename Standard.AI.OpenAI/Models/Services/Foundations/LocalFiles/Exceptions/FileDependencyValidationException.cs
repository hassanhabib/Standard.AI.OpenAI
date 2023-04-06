// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class FileDependencyValidationException : Xeption
    {
        public FileDependencyValidationException(Xeption innerException)
            : base(message: "File dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
