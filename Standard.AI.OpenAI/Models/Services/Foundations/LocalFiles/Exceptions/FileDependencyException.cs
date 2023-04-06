// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class FileDependencyException : Xeption
    {
        public FileDependencyException(Xeption innerException)
            : base(message: "File dependency error occurred, contact support.", 
                  innerException)
        { }
    }
}
