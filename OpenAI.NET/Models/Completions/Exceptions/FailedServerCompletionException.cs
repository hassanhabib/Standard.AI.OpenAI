// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OpenAI.NET.Models.Completions.Exceptions
{
    internal class FailedServerCompletionException : Xeption
    {
        public FailedServerCompletionException(Exception innerException)
            : base(message: "Failed server completion error occurred, contact support.", innerException)
        { }
    }
}
