// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class FailedFileServiceException : Xeption
    {
        public FailedFileServiceException(Exception innerException)
            : base(message: "Failed file service error occurred, contact support.",
                  innerException)
        { }
    }
}