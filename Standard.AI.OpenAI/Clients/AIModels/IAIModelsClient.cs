// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;

namespace Standard.AI.OpenAI.Clients.AIModels
{
    public interface IAIModelsClient
    {
        /// <summary>
        /// Lists the currently available models, and provides basic information
        /// about each one such as the owner and availability.
        /// </summary>
        /// <returns>
        /// A <see cref="ValueTask{TResult}"/> that represents the result of the
        /// asynchronous request, a list of <see cref="AIModel"/>s which
        /// represents the basic information about the currently available
        /// models.
        /// </returns>
        /// <exception cref="AIModelClientValidationException" />
        /// <exception cref="AIModelClientDependencyException" />
        /// <exception cref="AIModelClientServiceException" />
        ValueTask<IEnumerable<AIModel>> RetrieveAIModelsAsync();

        /// <exception cref="AIModelClientValidationException" />
        /// <exception cref="AIModelClientDependencyException" />
        /// <exception cref="AIModelClientServiceException" />
        ValueTask<AIModel> RetrieveAIModelByNameAsync(string aiModelName);
    }
}
