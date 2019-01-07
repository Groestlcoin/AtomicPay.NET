using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class InvoiceInfoDetails : InvoiceInfo
    {
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

        [JsonProperty("payment_currency")]
        public string PaymentCurrency { get; set; }

        [JsonProperty("payment_rate")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentRate { get; set; }

        [JsonProperty("payment_address")]
        public string PaymentAddress { get; set; }

        [JsonProperty("payment_total")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentTotal { get; set; }

        [JsonProperty("payment_paid")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentPaid { get; set; }

        [JsonProperty("payment_due")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentDue { get; set; }

        [JsonProperty("payment_txid")]
        public string PaymentTxid { get; set; }

        [JsonProperty("payment_confirmation")]
        public string PaymentConfirmation { get; set; }

        [JsonProperty("notification_email")]
        public string NotificationEmail { get; set; }

        [JsonProperty("notification_url")]
        public string NotificationUrl { get; set; }

        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        [JsonProperty("statusException")]
        [JsonConverter(typeof(StringToInvoiceStatusExceptionConverter))]
        public InvoiceStatusException StatusException { get; set; }
    }
}
