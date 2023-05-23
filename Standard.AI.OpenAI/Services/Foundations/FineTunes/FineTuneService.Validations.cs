// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal partial class FineTuneService
    {
        private static void ValidateFineTune(FineTune fineTune)
        {
            ValidateFineTuneNotNull(fineTune);
        }

        private static void ValidateFineTuneNotNull(FineTune fineTune)
        {
            if (fineTune is null)
            {
                throw new NullFineTuneException();
            }
        }
    }
}
