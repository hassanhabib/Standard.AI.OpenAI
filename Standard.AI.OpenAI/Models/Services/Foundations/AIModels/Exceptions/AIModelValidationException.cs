// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class AIModelValidationException : Xeption
    {
        public AIModelValidationException(Xeption innerException)
            : base(message: "AI Model validation error occurred, fix errors and try again.",
                innerException)
        { }
    }
}
