// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class LocalFileValidationException : Xeption
    {
        public LocalFileValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}