// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FineTuneDependencyValidationException : Xeption
    {
        public FineTuneDependencyValidationException(Xeption innerException)
            : base(message: "Fine tune dependency validation error occurred, fix errors and try again",
                  innerException)
        { }
    }
}
