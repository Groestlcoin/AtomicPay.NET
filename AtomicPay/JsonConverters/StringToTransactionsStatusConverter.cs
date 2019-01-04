using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToTransactionsStatusConverter : JsonConverter
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
                    return TransactionStatus.All;
                case "new":
                    return TransactionStatus.New;
                case "paid":
                    return TransactionStatus.Paid;
                case "confirmed":
                    return TransactionStatus.Confirmed;
                case "complete":
                    return TransactionStatus.Complete;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type TransactionStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null || (TransactionStatus)untypedValue == TransactionStatus.All)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(TransactionStatus), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToTransactionsStatusConverter Instance = new StringToTransactionsStatusConverter();
    }
}
