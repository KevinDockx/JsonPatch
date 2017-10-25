using System;
using Newtonsoft.Json;

namespace Marvin.JsonPatch.Internal
{
    public static class ConversionResultProvider
    {
        public static ConversionResult ConvertTo(object value, Type typeToConvertTo)
        {
            if (value == null)
            {
                return new ConversionResult(IsNullableType(typeToConvertTo), null);
            }
            else if (typeToConvertTo.IsAssignableFrom(value.GetType()))
            {
                // No need to convert
                return new ConversionResult(true, value);
            }
            else
            {
                try
                {
                    var deserialized = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), typeToConvertTo);
                    return new ConversionResult(true, deserialized);
                }
                catch
                {
                    return new ConversionResult(canBeConverted: false, convertedInstance: null);
                }
            }
        }

        public static ConversionResult CopyTo(object value, Type typeToConvertTo)
        {
            var targetType = typeToConvertTo;
            if (value == null)
            {
                return new ConversionResult(IsNullableType(typeToConvertTo), null);
            }
            else if (typeToConvertTo.IsAssignableFrom(value.GetType()))
            {
                // Keep original type
                targetType = value.GetType();
            }
            try
            {
                var deserialized = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), targetType);
                return new ConversionResult(true, deserialized);
            }
            catch
            {
                return new ConversionResult(canBeConverted: false, convertedInstance: null);
            }
        }

        private static bool IsNullableType(Type type)
        {
            if (type.IsValueType)
            {
                // value types are only nullable if they are Nullable<T>
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }
            else
            {
                // reference types are always nullable
                return true;
            }
        }
    }
}
