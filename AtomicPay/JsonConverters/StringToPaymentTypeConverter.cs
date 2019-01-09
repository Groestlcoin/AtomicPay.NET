using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToPaymentTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PaymentType);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            if (string.IsNullOrWhiteSpace(value)) return PaymentType.Default;

            switch(value.ToLowerInvariant())
            {
                case "default":
                    return PaymentType.Default;
                case "atomicpay":
                    return PaymentType.AtomicPay;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type PaymentType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(PaymentType), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToAccountStatusConverter Instance = new StringToAccountStatusConverter();
    }
}
