using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class PayUrlInfoDetails : PayUrlInfo
    {
        [JsonProperty("url_expiry")]
        [JsonConverter(typeof(StringToPayUrlExpiryConverter))]
        public PayUrlExpiry UrlExpiry { get; set; }

        [JsonProperty("order_id")]
        [JsonConverter(typeof(StringToLongConverter))]
        public long OrderId { get; set; }

        [JsonProperty("order_description")]
        public string OrderDescription { get; set; }

        [JsonProperty("order_price")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal OrderPrice { get; set; }

        [JsonProperty("order_currency")]
        public string OrderCurrency { get; set; }

        [JsonProperty("transaction_speed")]
        [JsonConverter(typeof(StringToTransactionSpeedConverter))]
        public TransactionSpeed TransactionSpeed { get; set; }

        [JsonProperty("notification_email")]
        public string NotificationEmail { get; set; }

        [JsonProperty("notification_url")]
        public Uri NotificationUrl { get; set; }

        [JsonProperty("redirect_url")]
        public Uri RedirectUrl { get; set; }
    }
}
