// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions
{
    [TypeConverter(typeof(ModelConverter))]
    public readonly struct Model
    {
        private readonly string value;

        public static Model Ada => "ada";
        public static Model AdaV1 => "text-ada-001";
        public static Model Babbage => "babbage";
        public static Model BabbageV1 => "text-babbage-001";
        public static Model Curie => "curie";
        public static Model CurieInstructBeta => "curie-instruct-beta";
        public static Model CurieV1 => "text-curie-001";
        public static Model Davinci => "davinci";
        public static Model DavinciInstructBeta => "davinci-instruct-beta";
        public static Model DavinciV1 => "text-davinci-001";
        public static Model DavinciV2 => "text-davinci-002";
        public static Model DavinciV3 => "text-davinci-003";
        public static Model CodexDavinciV2 => "code-davinci-002";
        public static Model CodexCushmanV1 => "code-cushman-001";

        private Model(string value)
        {
            this.value = value;
        }

        public static implicit operator Model(string value)
        {
            return new Model(value);
        }

        public static implicit operator string(Model model)
        {
            return model.value;
        }

        public override string ToString() => this;

        private class ModelConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(string);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                return value.ToString();
            }
        }
    }
}
