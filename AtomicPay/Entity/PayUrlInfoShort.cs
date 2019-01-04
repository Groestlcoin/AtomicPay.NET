using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;

namespace AtomicPay.Entity
{
    public class PayUrlInfoShort
    {
        [JsonProperty("url_timestamp")]
        [JsonConverter(typeof(StringToDateTimeOffsetConverter))]
        public DateTimeOffset UrlTimestamp { get; set; }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }

        [JsonProperty("url_id")]
        public string UrlId { get; set; }

        [JsonProperty("url_status")]
        [JsonConverter(typeof(StringToPayUrlStatusConverter))]
        public PayUrlStatus Status { get; set; }
    }
}