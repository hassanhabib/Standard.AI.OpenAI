// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions
{
    public class FineTuneClientServiceException : Xeption
    {
        public FineTuneClientServiceException(Xeption innerException)
            : base(message: "Fine tune client service error occurred, contact support.",
                  innerException)
        { }
    }
}
