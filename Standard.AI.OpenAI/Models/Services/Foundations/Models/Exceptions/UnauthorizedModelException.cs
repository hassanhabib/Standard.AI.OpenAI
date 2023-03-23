// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions
{
    internal class UnauthorizedModelException : Xeption
    {
        public UnauthorizedModelException(Exception innerException)
            : base(message: "Unauthorized models request, fix errors and try again.",
                  innerException)
        { }
    }
}