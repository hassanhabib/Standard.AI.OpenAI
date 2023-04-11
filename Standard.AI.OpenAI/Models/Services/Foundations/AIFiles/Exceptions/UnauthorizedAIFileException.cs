// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class UnauthorizedAIFileException : Xeption
    {
        public UnauthorizedAIFileException(Exception innerException)
            : base(message: "Unauthorized AI file request, fix errors and try again.",
                  innerException)
        { }
    }
}