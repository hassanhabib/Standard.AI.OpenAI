// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions
{
    public class CompletionServiceException : Xeption
    {
        public CompletionServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}