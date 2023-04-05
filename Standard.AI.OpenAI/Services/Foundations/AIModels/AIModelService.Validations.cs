using System;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AIModels
{
    internal partial class AIModelService
    {
        private void ValidateAIModelName(string aiModelName)
        {
            if (String.IsNullOrWhiteSpace(aiModelName))
            {
                throw new InvalidAIModelException();
            }
        }
    }
}
