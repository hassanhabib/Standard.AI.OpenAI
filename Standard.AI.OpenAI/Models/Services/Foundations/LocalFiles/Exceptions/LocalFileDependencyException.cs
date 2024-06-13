// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class LocalFileDependencyException : Xeption
    {
        public LocalFileDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}