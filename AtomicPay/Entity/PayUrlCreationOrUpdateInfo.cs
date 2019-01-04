using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class PayUrlCreationOrUpdateInfo
    {
        public PayUrlCreationOrUpdateInfo(string name, string currency, TransactionSpeed transactionSpeed = TransactionSpeed.Medium, PayUrlExpiry urlExpiry = PayUrlExpiry.None, long? orderId = null, decimal? orderprice = null, string description = null, string notificationemail = null, string notificationUrl = null, string redirecturl = null)
        {
            this.UrlName = name;
            this.OrderCurrency = currency;
            this.TransactionSpeed = transactionSpeed;
            this.UrlExpiry = urlExpiry;
            this.OrderId = orderId;
            this.OrderPrice = orderprice;
            this.NotificationEmail = notificationemail;
            this.NotificationUrl = notificationUrl;
            this.RedirectUrl = redirecturl;
        }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }

        [JsonProperty("order_currency")]
        public string OrderCurrency { get; set; }

        [JsonProperty("transaction_speed")]
        [JsonConverter(typeof(StringToTransactionSpeedConverter))]
        public TransactionSpeed TransactionSpeed { get; set; }

        [JsonProperty("url_expiry")]
        [JsonConverter(typeof(StringToPayUrlExpiryConverter))]
        public PayUrlExpiry UrlExpiry { get; set; }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToLongConverter))]
        public long? OrderId { get; set; }

        [JsonProperty("order_price", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal? OrderPrice { get; set; }

        [JsonProperty("order_description", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDescription { get; set; }

        [JsonProperty("notification_email", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationEmail { get; set; }

        [JsonProperty("notification_url", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationUrl { get; set; }

        [JsonProperty("redirect_url", NullValueHandling = NullValueHandling.Ignore)]
        public string RedirectUrl { get; set; }
    }
}
