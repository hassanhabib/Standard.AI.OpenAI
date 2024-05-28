// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public class FailedServerAIFileException : Xeption
    {

        public FailedServerAIFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}