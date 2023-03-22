// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    internal class NotFoundCompletionException : Xeption
    {
        public NotFoundCompletionException(Exception innerException)
            : base(message: "Not found completion error occurred. Fix the errors and try again.", innerException)
        { }
    }
}