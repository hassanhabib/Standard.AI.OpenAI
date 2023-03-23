// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class InvalidConfigurationModelException : Xeption
    {
        public InvalidConfigurationModelException(Exception innerException)
            : base(message: "Invalid configuration error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
