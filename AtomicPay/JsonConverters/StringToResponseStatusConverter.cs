using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToResponseStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ResponseStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            if (long.TryParse(value, out var l))
            {
                return (ResponseStatus)l;
            }

            throw new Exception("Cannot unmarshal type ResponseStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (int)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly StringToResponseStatusConverter Instance = new StringToResponseStatusConverter();
    }
}
