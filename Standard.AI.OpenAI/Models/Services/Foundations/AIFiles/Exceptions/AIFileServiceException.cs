// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileServiceException : Xeption
    {
        public AIFileServiceException(Xeption innerException)
            : base(message: "AI file service error occurred, contact support.",
                  innerException)
        { }
    }
}