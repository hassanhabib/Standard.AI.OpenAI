// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class InvalidAIFileException : Xeption
    {
        public InvalidAIFileException(string message)
            : base(message)
        { }


        public InvalidAIFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}