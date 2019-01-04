using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToInvoiceStatusExceptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(InvoiceStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch (value.ToLowerInvariant())
            {
                case null:
                case "":
                    return InvoiceStatusException.None;
                case "underpaid":
                    return InvoiceStatusException.Underpaid;
                case "overpaid":
                    return InvoiceStatusException.Overpaid;
                case "paid after expiry":
                    return InvoiceStatusException.Paid_After_Expiry;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type InvoiceStatusExcpetion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null || (InvoiceStatusException)untypedValue == InvoiceStatusException.None)
            {
                serializer.Serialize(writer, null);
                return;
            }
            
            var value = Enum.GetName(typeof(InvoiceStatusException), untypedValue);
            serializer.Serialize(writer, value.ToString().Replace("_", " ").ToLowerInvariant());
            return;
        }

        public static readonly StringToInvoiceStatusExceptionConverter Instance = new StringToInvoiceStatusExceptionConverter();
    }
}


