// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class NotFoundLocalFileException : Xeption
    {
        public NotFoundLocalFileException(Exception innerException)
            : base(message: "Not found local file error occurred, fix error and try again.",
                  innerException)
        { }
    }
}
