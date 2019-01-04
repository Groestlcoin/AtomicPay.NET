using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToBillingStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BillingStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch(value.ToLowerInvariant())
            {
                case "all":
                    return BillingStatus.All;
                case "pending":
                    return BillingStatus.Pending;
                case "paid":
                    return BillingStatus.Paid;
                case "overdue":
                    return BillingStatus.Overdue;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type BillingStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(BillingStatus), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToBillingStatusConverter Instance = new StringToBillingStatusConverter();
    }
}
