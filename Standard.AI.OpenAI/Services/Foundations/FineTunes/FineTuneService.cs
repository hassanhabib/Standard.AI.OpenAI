// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal partial class FineTuneService : IFineTuneService
    {
        private readonly IOpenAIBroker openAIBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public FineTuneService(IOpenAIBroker openAIBroker, IDateTimeBroker dateTimeBroker)
        {
            this.openAIBroker = openAIBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune) =>
        TryCatch(async () =>
        {
            ValidateFineTune(fineTune);

            ExternalFineTuneRequest externalFineTuneRequest =
                ConvertToFineTuneRequest(fineTune);

            ExternalFineTuneResponse externalFineTuneResponse =
                await this.openAIBroker.PostFineTuneAsync(externalFineTuneRequest);

            return ConvertToFineTune(fineTune, externalFineTuneResponse);
        });

        private static ExternalFineTuneRequest ConvertToFineTuneRequest(FineTune fineTune)
        {
            return new ExternalFineTuneRequest
            {
                FileId = fineTune.Request.FileId,
                ValidationFile = fineTune.Request.ValidationFile,
                Model = fineTune.Request.Model,
                NumberOfDatasetCycles = fineTune.Request.NumberOfDatasetCycles,
                BatchSize = fineTune.Request.BatchSize,
                LearningRateMultiplier = fineTune.Request.LearningRateMultiplier,
                PromptLossWeight = fineTune.Request.PromptLossWeight,
                ComputeClassificationMetrics = fineTune.Request.ComputeClassificationMetrics,
                NumberOfClasses = fineTune.Request.NumberOfClasses,
                ClassificationPositiveClass = fineTune.Request.ClassificationPositiveClass,
                Suffix = fineTune.Request.Suffix,
                ClassificationBetas = fineTune.Request.ClassificationBetas
            };
        }

        private FineTune ConvertToFineTune(
            FineTune fineTune,
            ExternalFineTuneResponse externalFineTuneResponse)
        {
            fineTune.Response = new FineTuneResponse
            {
                Id = externalFineTuneResponse.Id,
                Type = externalFineTuneResponse.Object,
                HyperParameters = ConvertToHyperParams(externalFineTuneResponse.HyperParameters),
                OrganizationId = externalFineTuneResponse.OrganizationId,
                Model = externalFineTuneResponse.Model,
                TrainingFiles = ConvertToTrainingFiles(externalFineTuneResponse.TrainingFiles),
                ValidationFiles = externalFineTuneResponse.ValidationFiles,
                ResultFiles = externalFineTuneResponse.ResultFiles,
                CreatedDate = ConvertToDateTime(externalFineTuneResponse.CreatedDate),
                UpdatedDate = ConvertToDateTime(externalFineTuneResponse.UpdatedDate),
                Status = externalFineTuneResponse.Status,
                FineTunedModel = externalFineTuneResponse.FineTunedModel,
                Events = ConvertToEvents(externalFineTuneResponse.Events),
            };

            return fineTune;
        }

        private Event[] ConvertToEvents(ExternalEvent[] events)
        {
            return events.Select(externalEvents => new Event
            {
                CreatedDate = ConvertToDateTime(externalEvents.CreatedDate),
                Level = externalEvents.Level,
                Message = externalEvents.Message,
                Type = externalEvents.Object
            }).ToArray();
        }

        private TrainingFile[] ConvertToTrainingFiles(ExternalTrainingFile[] trainingFiles)
        {
            return trainingFiles.Select(externalTrainingFile => new TrainingFile
            {
                Id = externalTrainingFile.Id,
                Type = externalTrainingFile.Object,
                Purpose = externalTrainingFile.Purpose,
                Filename = externalTrainingFile.Filename,
                Bytes = externalTrainingFile.Bytes,
                CreatedDate = ConvertToDateTime(externalTrainingFile.CreatedDate),
                Status = externalTrainingFile.Status,
                StatusDetails = externalTrainingFile.StatusDetails
            }).ToArray();
        }

        private static HyperParameter ConvertToHyperParams(ExternalHyperParameters hyperParameters)
        {
            return new HyperParameter
            {
                BatchSize = hyperParameters.BatchSize,
                EpochsCount = hyperParameters.EpochsCount,
                LearningRateMultiplier = hyperParameters.LearningRateMultiplier,
                PromptLossWeight = hyperParameters.PromptLossWeight
            };
        }

        private DateTimeOffset ConvertToDateTime(int createdDate) =>
            this.dateTimeBroker.ConvertToDateTimeOffSet(createdDate);
    }
}
