// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions
{
    internal class NotFoundLocalFileException : Xeption
    {
        public NotFoundLocalFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
