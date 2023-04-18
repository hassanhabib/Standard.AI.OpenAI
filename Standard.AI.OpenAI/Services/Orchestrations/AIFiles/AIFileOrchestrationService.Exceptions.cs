// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Orchestrations.AIFiles.Exceptions;

namespace Standard.AI.OpenAI.Services.Orchestrations.AIFiles
{
    internal partial class AIFileOrchestrationService
    {
        private delegate ValueTask<AIFile> ReturningAIFileFunction();

        private async ValueTask<AIFile> TryCatch(ReturningAIFileFunction returningAIFileFunction)
        {
            try
            {
                return await returningAIFileFunction();
            }
            catch (NullAIFileOrchestrationException nullAIFileOrchestrationException)
            {
                throw new AIFileOrchestrationValidationException(
                    nullAIFileOrchestrationException);
            }
            catch (InvalidAIFileOrchestrationException invalidAIFileOrchestrationException)
            {
                throw new AIFileOrchestrationValidationException(invalidAIFileOrchestrationException);
            }
        }
    }
}
