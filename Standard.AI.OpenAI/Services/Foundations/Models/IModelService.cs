// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;

namespace Standard.AI.OpenAI.Services.Foundations.Models
{
    internal interface IModelService
    {
        ValueTask<Model[]> GetModelsAsync();
    }
}