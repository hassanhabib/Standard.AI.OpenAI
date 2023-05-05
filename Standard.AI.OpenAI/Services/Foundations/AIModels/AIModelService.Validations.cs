// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal partial class AIModelService
    {
        private static void ValidateAIModelName(string aiModelName) =>
            Validate((Rule: IsInvalidName(aiModelName), Parameter: nameof(AIModel.Name)));

        private static dynamic IsInvalidName(string aiModelName) => new
        {
            Condition = String.IsNullOrWhiteSpace(aiModelName),
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAIModelException = new InvalidAIModelException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAIModelException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAIModelException.ThrowIfContainsErrors();
        }
    }
}