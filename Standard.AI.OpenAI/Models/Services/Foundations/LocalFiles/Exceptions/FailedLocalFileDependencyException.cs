// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class FailedLocalFileDependencyException : Xeption
    {
        public FailedLocalFileDependencyException(Exception innerException)
            : base(message: "Failed local file error occurred, contact support.", innerException)
        { }
    }
}
