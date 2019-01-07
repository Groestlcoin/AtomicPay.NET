using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class PayUrlStatusInfo : EntityBase
    {
        [JsonProperty("url_id")]
        public string UrlId { get; set; }

        [JsonProperty("url_status")]
        [JsonConverter(typeof(StringToPayUrlStatusConverter))]
        public PayUrlStatus Status { get; set; }
    }
}
