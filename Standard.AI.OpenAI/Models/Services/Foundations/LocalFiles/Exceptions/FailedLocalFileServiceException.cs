// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class FailedLocalFileServiceException : Xeption
    {
        public FailedLocalFileServiceException(Exception innerException)
            : base(message: "Failed local file service error occurred, contact support.", innerException)
        { }
    }
}
