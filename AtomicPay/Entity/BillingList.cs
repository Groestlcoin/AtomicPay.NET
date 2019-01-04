using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class BillingList<TBillInfo> : EntityBase where TBillInfo : BillInfoShort
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long Count { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<TBillInfo> BillList { get; set; }
    }
}
