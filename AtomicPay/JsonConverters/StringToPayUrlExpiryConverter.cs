using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToPayUrlExpiryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PayUrlExpiry);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch (value.ToLowerInvariant())
            {
                case null:
                case "0":
                    return PayUrlExpiry.None;
                case "1":
                    return PayUrlExpiry.OneDay;
                case "7":
                    return PayUrlExpiry.SevenDays;
                case "15":
                    return PayUrlExpiry.FifteenDays;
                case "30":
                    return PayUrlExpiry.ThirdyDays;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type PayUrlExpiry");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            string value = null;

            switch ((PayUrlExpiry)untypedValue)
            {
                case PayUrlExpiry.None:
                    value = "0";
                    break;
                case PayUrlExpiry.OneDay:
                    value = "1";
                    break;
                case PayUrlExpiry.SevenDays:
                    value = "7";
                    break;
                case PayUrlExpiry.FifteenDays:
                    value = "15";
                    break;
                case PayUrlExpiry.ThirdyDays:
                    value = "30";
                    break;
            }

            serializer.Serialize(writer, value.Replace("_", " ").ToLowerInvariant());

            return;
        }

        public static readonly StringToPayUrlExpiryConverter Instance = new StringToPayUrlExpiryConverter();
    }
}


