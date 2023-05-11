// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal interface IFineTuneService
    {
        ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune);
    }
}
