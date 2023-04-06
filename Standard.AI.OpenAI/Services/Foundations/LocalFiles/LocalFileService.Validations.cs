// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.LocalFiles
{
    internal partial class LocalFileService
    {
        private void ValidatePath(string path)
        {
            Validate(
                (Rule: IsInvalid(path), Parameter: "FilePath"));
        }

        private dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidFileException = new InvalidLocalFileException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidFileException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidFileException.ThrowIfContainsErrors();
        }
    }
}
