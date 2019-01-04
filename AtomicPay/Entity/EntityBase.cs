using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class EntityBase
    {
        [JsonProperty("code")]
        [JsonConverter(typeof(StringToResponseStatusConverter))]
        public ResponseStatus Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
