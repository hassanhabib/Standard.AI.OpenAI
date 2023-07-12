// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.FineTunes.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;

namespace Standard.AI.OpenAI.Clients.FineTunes
{
    public interface IFineTuneClient
    {
        /// <exception cref="FineTuneClientValidationException"/>
        /// <exception cref="FineTuneClientDependencyException"/>
        /// <exception cref="FineTuneClientServiceException"/>
        ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune);
    }
}
