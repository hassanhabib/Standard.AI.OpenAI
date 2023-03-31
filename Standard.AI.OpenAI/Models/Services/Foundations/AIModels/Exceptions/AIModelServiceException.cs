// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public class AIModelServiceException : Xeption
    {

        public AIModelServiceException(Xeption innerException)
            : base(message: "AI Model service error occurred, contact support.", innerException)
        { }
    }
}