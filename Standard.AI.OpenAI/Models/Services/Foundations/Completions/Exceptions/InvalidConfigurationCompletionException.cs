// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class InvalidConfigurationCompletionException : Xeption
    {
        public InvalidConfigurationCompletionException(Exception innerException)
            : base(message: "Invalid configuration error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
