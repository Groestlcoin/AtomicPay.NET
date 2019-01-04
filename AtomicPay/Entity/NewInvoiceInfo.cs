using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class NewInvoiceInfo : EntityBase
    {
        [JsonProperty("invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceId { get; set; }

        [JsonProperty("invoice_url", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceUrl { get; set; }
    }
}
