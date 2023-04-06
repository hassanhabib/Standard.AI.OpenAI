// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class InvalidFileException : Xeption
    {
        public InvalidFileException()
            : base(message: "Invalid file error occurred, fix errors and try again.")
        { }

        public InvalidFileException(Exception innerException)
            : base(message: "File is invalid",
                  innerException)
        { }
    }
}