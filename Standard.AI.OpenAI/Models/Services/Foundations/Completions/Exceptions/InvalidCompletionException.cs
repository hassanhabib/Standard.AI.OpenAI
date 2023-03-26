// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class InvalidCompletionException : Xeption
    {
        public InvalidCompletionException()
            : base(message: "Invalid completion error occurred, fix errors and try again.")
        { }

        public InvalidCompletionException(Exception innerException)
            : base(message: "Invalid completion error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
