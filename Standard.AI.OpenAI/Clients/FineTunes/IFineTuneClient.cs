// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;

namespace Standard.AI.OpenAI.Clients.FineTunes
{
    public interface IFineTuneClient
    {
        ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune);
    }
}
