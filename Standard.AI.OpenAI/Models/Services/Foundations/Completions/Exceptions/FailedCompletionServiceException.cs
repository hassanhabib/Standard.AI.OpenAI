// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class FailedCompletionServiceException : Xeption
    {
        public FailedCompletionServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}