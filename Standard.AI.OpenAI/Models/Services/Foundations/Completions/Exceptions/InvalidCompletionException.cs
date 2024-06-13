// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class InvalidCompletionException : Xeption
    {
        public InvalidCompletionException(string message)
            : base(message)
        { }

        public InvalidCompletionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}