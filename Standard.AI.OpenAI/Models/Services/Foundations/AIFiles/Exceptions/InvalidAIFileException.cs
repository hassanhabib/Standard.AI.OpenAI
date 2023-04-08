// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class InvalidAIFileException : Xeption
    {
        public InvalidAIFileException()
            : base(message: "Invalid AI file error occurred, fix errors and try again.")
        { }

        public InvalidAIFileException(Exception innerException)
            : base(message: "Invalid AI file error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}