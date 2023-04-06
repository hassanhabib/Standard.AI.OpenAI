// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    public class LocalFileServiceException : Xeption
    {
        public LocalFileServiceException(Xeption innerException)
            : base(message: "Local file service error occurred, contact support.", innerException)
        { }
    }
}
