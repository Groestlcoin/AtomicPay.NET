using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToDecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(decimal) || t == typeof(decimal?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return default(decimal);
            var value = serializer.Deserialize<string>(reader);
            if (decimal.TryParse(value, out var d))
            {
                return d;
            }

            return default(decimal);
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (decimal)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly StringToDecimalConverter Instance = new StringToDecimalConverter();
    }
}
