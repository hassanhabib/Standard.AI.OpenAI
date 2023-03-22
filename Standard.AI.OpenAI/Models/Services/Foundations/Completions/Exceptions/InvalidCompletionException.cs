// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OpenAI.NET.Models.Services.Foundations.Completions.Exceptions
{
    internal class InvalidCompletionException : Xeption
    {
        public InvalidCompletionException()
            : base(message: "Invalid completion error occurred, fix errors and try again.")
        { }

        public InvalidCompletionException(Exception innerException)
            : base(message: "Invalid completion error occurred, fix errors and try again.", innerException)
        { }
    }
}
