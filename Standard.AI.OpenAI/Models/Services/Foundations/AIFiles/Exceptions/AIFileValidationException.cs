// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileValidationException : Xeption
    {
        public AIFileValidationException(Xeption innerException)
            : base(message: "AI file validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}