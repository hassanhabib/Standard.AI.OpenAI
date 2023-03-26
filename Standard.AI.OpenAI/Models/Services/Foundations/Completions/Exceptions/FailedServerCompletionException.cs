// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class FailedServerCompletionException : Xeption
    {
        public FailedServerCompletionException(Exception innerException)
            : base(message: "Failed server completion error occurred, contact support.",
                  innerException)
        { }
    }
}
