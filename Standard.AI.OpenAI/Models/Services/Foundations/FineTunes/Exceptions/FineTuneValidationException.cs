// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FineTuneValidationException : Xeption
    {
        public FineTuneValidationException(Xeption innerException)
            : base(message: "Fine tune validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
