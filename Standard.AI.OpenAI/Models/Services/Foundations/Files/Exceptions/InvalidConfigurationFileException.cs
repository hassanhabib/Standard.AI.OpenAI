// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class InvalidConfigurationFileException : Xeption
    {
        public InvalidConfigurationFileException(Exception innerException)
            : base(message: "Invalid file configuration error occurred, contact support.",
                  innerException)
        { }
    }
}