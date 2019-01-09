using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class BillingList<TBillInfo> : EntityBase where TBillInfo : BillInfo
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long Count { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<TBillInfo> Result { get; set; }
    }
}
