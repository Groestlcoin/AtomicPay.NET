using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToTransactionSpeedConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TransactionSpeed);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch(value.ToLowerInvariant())
            {
                case "high":
                    return TransactionSpeed.High;
                case "medium":
                    return TransactionSpeed.Medium;
                case "low":
                    return TransactionSpeed.Low;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type TransactionSpeed");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(TransactionSpeed), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToTransactionSpeedConverter Instance = new StringToTransactionSpeedConverter();
    }
}
