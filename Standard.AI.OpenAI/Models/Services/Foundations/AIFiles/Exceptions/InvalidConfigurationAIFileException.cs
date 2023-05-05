// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class InvalidConfigurationAIFileException : Xeption
    {
        public InvalidConfigurationAIFileException(Exception innerException)
            : base(message: "Invalid AI file configuration error occurred, contact support.",
                  innerException)
        { }
    }
}