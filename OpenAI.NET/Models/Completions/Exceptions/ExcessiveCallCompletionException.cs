// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OpenAI.NET.Models.Completions.Exceptions
{
    internal class ExcessiveCallCompletionException : Xeption
    {
        public ExcessiveCallCompletionException(Exception innerException)
            : base(message: "Excessive call error occurred. Limit your calls", innerException)
        { }
    }
}