using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToInvoiceStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(InvoiceStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch (value.ToLowerInvariant())
            {
                case "all":
                    return InvoiceStatus.All;
                case "new":
                    return InvoiceStatus.New;
                case "paid":
                    return InvoiceStatus.Paid;
                case "confirmed":
                    return InvoiceStatus.Confirmed;
                case "complete":
                    return InvoiceStatus.Complete;
                case "expired":
                    return InvoiceStatus.Expired;
                case "invalid":
                    return InvoiceStatus.Invalid;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type InvoiceStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(InvoiceStatus), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToInvoiceStatusConverter Instance = new StringToInvoiceStatusConverter();
    }
}
