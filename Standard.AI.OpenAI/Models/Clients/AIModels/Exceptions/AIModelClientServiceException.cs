// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    public class AIModelClientServiceException : Xeption
    {
        public AIModelClientServiceException(Xeption innerException)
            : base(message: "AI Model client service error occurred, contact support.", innerException)
        { }
    }
}
