// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal partial class FileService
    {
        private static void ValidateFileId(string fileId) =>
            Validate((Rule: IsInvalid(fileId), Parameter: nameof(File.Id)));

        private static dynamic IsInvalid(string fileId) => new
        {
            Condition = String.IsNullOrWhiteSpace(fileId),
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidFileException = new InvalidFileException();

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
