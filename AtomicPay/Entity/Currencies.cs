using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class Currencies : EntityBase
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("result")]
        public List<CurrencyInfo> Result { get; set; }
    }
}
