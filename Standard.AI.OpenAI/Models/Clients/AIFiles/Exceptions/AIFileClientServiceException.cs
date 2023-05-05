// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIFiles.Exceptions
{
    public class AIFileClientServiceException : Xeption
    {
        public AIFileClientServiceException(Xeption innerException)
            : base(message: "AI file client service error occurred, contact support.", innerException)
        { }
    }
}
