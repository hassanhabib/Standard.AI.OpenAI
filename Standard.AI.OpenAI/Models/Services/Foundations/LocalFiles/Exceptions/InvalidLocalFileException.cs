// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class InvalidLocalFileException : Xeption
    {
        public InvalidLocalFileException()
            : base(message: "Invalid local file error occurred, fix error and try again.")
        { }

        public InvalidLocalFileException(Exception innerException)
            : base(message: "Invalid local file error occurred, fix error and try again.", innerException)
        { }
    }
}
