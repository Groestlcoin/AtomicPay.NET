using AtomicPay.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.JsonConverters
{
    public class StringToAccountStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AccountStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            switch(value.ToLowerInvariant())
            {
                case "pending":
                    return AccountStatus.Pending;
                case "active":
                    return AccountStatus.Active;
                case "locked":
                    return AccountStatus.Locked;
                case "suspended":
                    return AccountStatus.Suspended;
                case "banned":
                    return AccountStatus.Banned;
                default:
                    throw new ArgumentOutOfRangeException($"value {value} is not yet implemented");
            }

            throw new Exception("Cannot unmarshal type AccountStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = Enum.GetName(typeof(AccountStatus), untypedValue);
            serializer.Serialize(writer, value.ToString().ToLowerInvariant());
            return;
        }

        public static readonly StringToAccountStatusConverter Instance = new StringToAccountStatusConverter();
    }
}
