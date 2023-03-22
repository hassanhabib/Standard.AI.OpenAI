// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.Completions.Exceptions
{
    internal class CompletionClientServiceException : Xeption
    {
        public CompletionClientServiceException(Exception innerException)
            : base(message: "Completion client service error occurred, contact support.", innerException)
        { }
    }
}
