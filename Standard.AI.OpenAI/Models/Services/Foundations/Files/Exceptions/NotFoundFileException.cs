// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class NotFoundFileException : Xeption
    {
        public NotFoundFileException(Exception innerException)
            : base(message: "Not found file error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}