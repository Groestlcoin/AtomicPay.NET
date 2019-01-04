using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class RateInfo
    {
        [JsonProperty("pair")]
        public string Pair { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
    }
}
