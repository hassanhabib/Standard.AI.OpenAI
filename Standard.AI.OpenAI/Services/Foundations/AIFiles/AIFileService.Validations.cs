// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIFiles
{
    internal partial class AIFileService
    {
        private static void ValidateFileId(string fileId) =>
            Validate((Rule: IsInvalid(fileId), Parameter: nameof(AIFile.Response.Id)));

        private static dynamic IsInvalid(string fileId) => new
        {
            Condition = String.IsNullOrWhiteSpace(fileId),
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAIFileException = new InvalidAIFileException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAIFileException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAIFileException.ThrowIfContainsErrors();
        }
    }
}