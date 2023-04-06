// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions
{
    public class UnauthorizedFileException : Xeption
    {
        public UnauthorizedFileException(Exception innerException)
            : base(message: "Unauthorized file request, fix errors and try again.",
                  innerException)
        { }
    }
}