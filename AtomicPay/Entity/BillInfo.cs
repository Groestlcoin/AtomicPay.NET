using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class BillInfo
    {
        [JsonProperty("bill_timestamp")]
        [JsonConverter(typeof(StringToDateTimeOffsetConverter))]
        public DateTimeOffset BillTimestamp { get; set; }

        [JsonProperty("bill_id")]
        [JsonConverter(typeof(StringToLongConverter))]
        public long BillId { get; set; }

        [JsonProperty("bill_month")]
        public string BillMonth { get; set; }

        [JsonProperty("bill_year")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int BillYear { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringToBillingStatusConverter))]
        public BillingStatus Status { get; set; }
    }
}
