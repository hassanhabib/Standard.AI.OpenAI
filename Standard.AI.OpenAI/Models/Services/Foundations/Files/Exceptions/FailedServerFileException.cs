// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class FailedServerFileException : Xeption
    {
        public FailedServerFileException(Exception innerException)
            : base(message: "Failed file server error occurred, contact support.",
                  innerException)
        { }
    }
}