// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions
{
    public class FineTuneClientValidationException : Xeption
    {
        public FineTuneClientValidationException(Xeption innerException)
            : base(message: "Fine tune client validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
