// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class FailedFileException : Xeption
    {
        public FailedFileException(Exception innerException)
            : base(message: "Failed file error occurred, contact support.", innerException)
        { }
    }
}
