// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions
{
    public class FineTuneClientDependencyException : Xeption
    {
        public FineTuneClientDependencyException(Xeption innerException)
            : base(message: "Fine tune client dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
