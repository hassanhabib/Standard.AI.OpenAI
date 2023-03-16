// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Completions.Exceptions
{
    internal class InvalidCompletionException : Xeption
    {
        public InvalidCompletionException()
            : base(message: "Invalid completion error occurred, fix errors and try again.")
        { }
    }
}
