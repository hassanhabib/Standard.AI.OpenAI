// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class FailedServerModelException : Xeption
    {
        public FailedServerModelException(Exception innerException)
            : base(message: "Failed server model error occurred, contact support.",
                  innerException)
        { }
    }
}
