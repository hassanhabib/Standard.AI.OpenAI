// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class FileDependencyValidationException : Xeption
    {
        public FileDependencyValidationException(Xeption innerException)
            : base(message: "File dependency validation error occurred, contact support.",
                  innerException)
        { }
    }
}