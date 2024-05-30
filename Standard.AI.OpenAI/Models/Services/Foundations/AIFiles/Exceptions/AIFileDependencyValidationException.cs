// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileDependencyValidationException : Xeption
    {
        public AIFileDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}