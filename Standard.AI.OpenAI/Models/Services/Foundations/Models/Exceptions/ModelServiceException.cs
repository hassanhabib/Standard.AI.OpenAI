// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class ModelServiceException : Xeption
    {
        public ModelServiceException(Xeption innerException)
            : base(message: "Model service error occurred, contact support.",
                  innerException)
        { }
    }
}