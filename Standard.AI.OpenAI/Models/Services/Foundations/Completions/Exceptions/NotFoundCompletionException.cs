// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class NotFoundCompletionException : Xeption
    {
        public NotFoundCompletionException(Exception innerException)
            : base(message: "Not found completion error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}