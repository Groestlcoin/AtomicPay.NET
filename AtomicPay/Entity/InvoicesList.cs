using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class InvoicesList<TInvoiceEntity> : EntityBase where TInvoiceEntity : InvoiceInfo
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<TInvoiceEntity> Result { get; set; }
    }
}
