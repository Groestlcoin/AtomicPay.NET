using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class InvoiceCreationInfo
    {
        public InvoiceCreationInfo(decimal price, string currency, long? orderId = null, string description = null, TransactionSpeed trxSpeed = TransactionSpeed.Medium, string notificationEmail = null, string notificationUrl = null, string redirectUrl = null)
        {
            this.OrderPrice = price;
            this.OrderCurrency = currency;
            this.OrderId = orderId;
            this.OrderDescription = description;
            this.TransactionSpeed = trxSpeed;
            this.NotificationEmail = notificationEmail;
            this.NotificationUrl = notificationUrl;
            this.RedirectUrl = redirectUrl;

            if (!string.IsNullOrWhiteSpace(this.RedirectUrl))
                this.Redirect = true;
            else
                this.Redirect = null;
        }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToLongConverter))]
        public long? OrderId { get; set; }

        [JsonProperty("order_price")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal OrderPrice { get; set; }

        [JsonProperty("order_currency")]
        public string OrderCurrency { get; set; }

        [JsonProperty("order_description", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDescription { get; set; }

        [JsonProperty("transaction_speed")]
        [JsonConverter(typeof(StringToTransactionSpeedConverter))]
        public TransactionSpeed TransactionSpeed { get; set; }

        [JsonProperty("notification_email", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationEmail { get; set; }

        [JsonProperty("notification_url", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationUrl { get; set; }

        [JsonProperty("redirect", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Redirect { get; set; }

        [JsonProperty("redirect_url", NullValueHandling = NullValueHandling.Ignore)]
        public string RedirectUrl { get; set; }
    }
}
