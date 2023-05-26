// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class FineTuneServiceException : Xeption
    {
        public FineTuneServiceException(Xeption innerException)
            : base(message: "Fine tune error ocurred, contact support.",
                  innerException)
        { }
    }
}
