// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileServiceException : Xeption
    {
        public AIFileServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}