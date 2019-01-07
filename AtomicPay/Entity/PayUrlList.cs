using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class PayUrlList<TPayUrlEntity> : EntityBase where TPayUrlEntity : PayUrlInfo
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<TPayUrlEntity> Result { get; set; }
    }
}
