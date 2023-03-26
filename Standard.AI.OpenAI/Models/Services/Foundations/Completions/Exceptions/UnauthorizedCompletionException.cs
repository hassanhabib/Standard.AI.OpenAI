// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class UnauthorizedCompletionException : Xeption
    {
        public UnauthorizedCompletionException(Exception innerException)
            : base(message: "Unauthorized completion request, fix errors and try again.",
                  innerException)
        { }
    }
}
