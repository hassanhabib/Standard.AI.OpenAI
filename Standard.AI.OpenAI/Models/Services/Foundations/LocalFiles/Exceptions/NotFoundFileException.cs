// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class NotFoundFileException : Xeption
    {
        public NotFoundFileException(Exception innerException)
            : base(message: "Not found file error occurred, fix error and try again.",
                  innerException)
        { }
    }
}
