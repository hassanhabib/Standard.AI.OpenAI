// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;
using Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.Models
{
    internal partial class ModelService : IModelService
    {
        private delegate ValueTask<Model[]> ReturningModelArrayFunction();

        private static async ValueTask<Model[]> TryCatch(ReturningModelArrayFunction returningModelArrayFunction)
        {
            try
            {
                return await returningModelArrayFunction();
            }
            catch (Exception exception)
            {
                var failedModelServiceException =
                    new FailedModelServiceException(exception);

                throw new ModelServiceException(failedModelServiceException);
            }
        }
    }
}