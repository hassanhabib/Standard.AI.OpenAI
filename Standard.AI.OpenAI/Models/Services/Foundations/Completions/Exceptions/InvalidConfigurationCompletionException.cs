// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OpenAI.NET.Models.Services.Foundations.Completions.Exceptions
{
    internal class InvalidConfigurationCompletionException : Xeption
    {
        public InvalidConfigurationCompletionException(Exception innerException)
            : base(message: "Invalid configuration error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
