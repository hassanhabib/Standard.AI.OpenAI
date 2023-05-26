// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal partial class FineTuneService
    {
        private delegate ValueTask<FineTune> ReturningFineTuneFunction();

        private static async ValueTask<FineTune> TryCatch(ReturningFineTuneFunction returningFineTuneFunction)
        {
            try
            {
                return await returningFineTuneFunction();
            }
            catch (NullFineTuneException nullFineTuneException)
            {
                throw new FineTuneValidationException(nullFineTuneException);
            }
            catch (InvalidFineTuneException invalidFineTuneException)
            {
                throw new FineTuneValidationException(invalidFineTuneException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidFineTuneConfigurationException =
                    new InvalidFineTuneConfigurationException(httpResponseUrlNotFoundException);

                throw new FineTuneDependencyException(invalidFineTuneConfigurationException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedFineTuneException =
                    new UnauthorizedFineTuneException(
                        httpResponseUnauthorizedException);

                throw new FineTuneDependencyException(unauthorizedFineTuneException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedFineTuneException =
                    new UnauthorizedFineTuneException(
                        httpResponseForbiddenException);

                throw new FineTuneDependencyException(unauthorizedFineTuneException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidFineTuneException =
                    new InvalidFineTuneException(
                        httpResponseBadRequestException);

                throw new FineTuneDependencyValidationException(invalidFineTuneException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallFineTuneException =
                    new ExcessiveCallFineTuneException(httpResponseTooManyRequestsException);

                throw new FineTuneDependencyValidationException(excessiveCallFineTuneException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerFineTuneException =
                    new FailedServerFineTuneException(httpResponseException);

                throw new FineTuneDependencyException(failedServerFineTuneException);
            }
            catch (Exception exception)
            {
                var failedFineTuneServiceException =
                    new FailedFineTuneServiceException(exception);

                throw new FineTuneServiceException(failedFineTuneServiceException);
            }
        }
    }
}
