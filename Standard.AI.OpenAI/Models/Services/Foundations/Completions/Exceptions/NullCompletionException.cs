// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace OpenAI.NET.Models.Services.Foundations.Completions.Exceptions
{
    public class NullCompletionException : Xeption
    {
        public NullCompletionException()
            : base(message: "Completion is null.") { }
    }
}
