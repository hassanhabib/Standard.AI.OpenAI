// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class FailedAIFileServiceException : Xeption
    {
        public FailedAIFileServiceException(Exception innerException)
            : base(message: "Failed AI file service error occurred, contact support.",
                  innerException)
        { }
    }
}