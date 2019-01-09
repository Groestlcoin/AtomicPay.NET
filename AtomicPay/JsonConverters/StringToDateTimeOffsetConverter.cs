using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToDateTimeOffsetConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            if (string.IsNullOrWhiteSpace(value)) return default(DateTimeOffset);

            if (long.TryParse(value, out var unixSecs))
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixSecs);
            }

            throw new Exception("Cannot unmarshal type DateTimeOFfset");
            
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = ((DateTimeOffset)untypedValue).ToUnixTimeSeconds();
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly StringToDateTimeOffsetConverter Instance = new StringToDateTimeOffsetConverter();
    }
}
