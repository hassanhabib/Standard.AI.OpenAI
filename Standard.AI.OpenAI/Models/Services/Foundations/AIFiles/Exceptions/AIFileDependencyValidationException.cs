// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileDependencyValidationException : Xeption
    {
        public AIFileDependencyValidationException(Xeption innerException)
            : base(message: "AI file dependency validation error occurred, contact support.",
                  innerException)
        { }
    }
}