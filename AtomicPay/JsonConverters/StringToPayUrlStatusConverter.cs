using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToPayUrlStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PayUrlStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch (value.ToLowerInvariant())
            {
                case null:
                case "":
                    return PayUrlStatus.All;
                case "active":
                    return PayUrlStatus.Active;
                case "expired":
                    return PayUrlStatus.Expired;
                case "deleted":
                    return PayUrlStatus.Deleted;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type PayUrlStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null || (PayUrlStatus)untypedValue == PayUrlStatus.All)
            {
                serializer.Serialize(writer, null);
                return;
            }
            
            var value = Enum.GetName(typeof(PayUrlStatus), untypedValue);
            serializer.Serialize(writer, value.ToString().Replace("_", " ").ToLowerInvariant());
            return;
        }

        public static readonly StringToPayUrlStatusConverter Instance = new StringToPayUrlStatusConverter();
    }
}


