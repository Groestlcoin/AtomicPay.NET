using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class InvoiceInfoShort : EntityBase
    {
        [JsonProperty("invoice_timestamp")]
        [JsonConverter(typeof(StringToDateTimeOffsetConverter))]
        public DateTimeOffset InvoiceTimestamp { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringToInvoiceStatusConverter))]
        public InvoiceStatus Status { get; set; }
    }
}
