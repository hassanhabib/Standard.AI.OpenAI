// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class LocalFileDependencyException : Xeption
    {
        public LocalFileDependencyException(Xeption innerException)
            : base(message: "Local file dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
