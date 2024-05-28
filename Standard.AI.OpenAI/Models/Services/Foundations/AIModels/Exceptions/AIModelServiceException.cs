// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    public class AIModelServiceException : Xeption
    {

        public AIModelServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}