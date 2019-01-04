using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class CurrencyInfo
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("precision")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Precision { get; set; }

        [JsonProperty("minimum")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Minimum { get; set; }
    }
}
