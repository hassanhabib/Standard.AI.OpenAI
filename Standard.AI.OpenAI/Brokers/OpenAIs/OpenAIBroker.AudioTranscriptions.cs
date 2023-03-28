// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalAudioTranscriptionResponse> PostAudioTranscriptionRequestAsync(
            ExternalAudioTranscriptionRequest externalAudioTranscriptionRequest)
        {
            string pathToFile = Path.GetFullPath(externalAudioTranscriptionRequest.FilePath);

            using MultipartFormDataContent formData = new();
            using FileStream fileStream = File.OpenRead(pathToFile);

            formData.Add(
                content: new StreamContent(fileStream),
                name: "file",
                fileName: Path.GetFileName(pathToFile));

            string requestAsString = JsonConvert.SerializeObject(
                value: externalAudioTranscriptionRequest,
                formatting: Formatting.None,
                settings: CreateJsonSerializerSettings(ignoreDefaultValues: true));

            foreach (KeyValuePair<string, string> item in
                JsonConvert.DeserializeObject<Dictionary<string, string>>(requestAsString))
            {
                formData.Add(
                    content: new StringContent(item.Value),
                    name: item.Key);
            }

            return await this.PostAsync<ExternalAudioTranscriptionResponse>(
                relativeUrl: "v1/audio/transcriptions",
                content: formData);

            static JsonSerializerSettings CreateJsonSerializerSettings(bool ignoreDefaultValues)
            {
                DefaultValueHandling defaultValueHandling = ignoreDefaultValues
                    ? DefaultValueHandling.Ignore
                    : DefaultValueHandling.Include;

                return new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
            }
        }
    }
}
