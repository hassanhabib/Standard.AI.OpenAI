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
        private static void ValidateAIFile(AIFile aiFile)
        {
            ValidateAIFileNotNull(aiFile);

            Validate(
                (Rule: IsInvalid(aiFile.Request), Parameter: nameof(AIFile.Request)));

            Validate(
                (Rule: IsInvalid(aiFile.Request.Name), Parameter: nameof(AIFileRequest.Name)),
                (Rule: IsInvalid(aiFile.Request.Content), Parameter: nameof(AIFileRequest.Content)),
                (Rule: IsInvalid(aiFile.Request.Purpose), Parameter: nameof(AIFileRequest.Purpose)));
        }

        private static void ValidateAIFileNotNull(AIFile aiFile)
        {
            if (aiFile is null)
            {
                throw new NullAIFileException();
            }
        }

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Value is required"
        };

        private static void ValidateFileId(string fileId) =>
            Validate((Rule: IsInvalid(fileId), Parameter: nameof(AIFile.Response.Id)));

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
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