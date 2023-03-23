// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;
using Xeptions;

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

    internal class ModelServiceException : Xeption
    {
        public ModelServiceException(Xeption innerException)
            : base(message: "Model service error occurred, contact support.",
                  innerException)
        { }
    }

    internal class FailedModelServiceException : Xeption
    {
        public FailedModelServiceException(Exception innerException)
            : base(message: "Failed model service error occurred, contact support.",
                   innerException)
        { }
    }
}