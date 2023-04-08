// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class AIFileDependencyException : Xeption
    {
        public AIFileDependencyException(Xeption innerException)
            : base(message: "AI file dependency error occurred, contact support.",
                  innerException)
        { }
    }
}