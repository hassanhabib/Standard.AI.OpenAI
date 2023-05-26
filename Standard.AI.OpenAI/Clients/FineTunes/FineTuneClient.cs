// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.FineTunes;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.FineTunes
{
    internal class FineTuneClient : IFineTuneClient
    {
        private readonly IFineTuneService fineTuneService;

        public FineTuneClient(IFineTuneService fineTuneService) =>
            this.fineTuneService = fineTuneService;

        public async ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune)
        {
            try
            {
                return await this.fineTuneService.SubmitFineTuneAsync(fineTune);
            }
            catch (FineTuneValidationException fineTuneValidationException)
            {
                throw new FineTuneClientValidationException(
                    fineTuneValidationException.InnerException as Xeption);
            }
            catch (FineTuneDependencyException fineTuneDependencyException)
            {
                throw new FineTuneClientDependencyException(
                    fineTuneDependencyException.InnerException as Xeption);
            }
            catch (FineTuneDependencyValidationException fineTuneDependencyValidationException)
            {
                throw new FineTuneClientValidationException(
                    fineTuneDependencyValidationException.InnerException as Xeption);
            }
            catch (FineTuneServiceException fineTuneServiceException)
            {
                throw new FineTuneClientServiceException(
                    fineTuneServiceException.InnerException as Xeption);
            }
        }
    }
}
