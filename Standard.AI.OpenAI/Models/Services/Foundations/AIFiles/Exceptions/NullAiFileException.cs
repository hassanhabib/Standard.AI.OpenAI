// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public partial class NullAIFileException : Xeption
    {
        public NullAIFileException(String message)
            : base(message)
        { }
        public NullAIFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}