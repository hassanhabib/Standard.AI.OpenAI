// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class FailedModelServiceException : Xeption
    {
        public FailedModelServiceException(Exception innerException)
            : base(message: "Failed model service error occurred, contact support.",
                   innerException)
        { }
    }
}