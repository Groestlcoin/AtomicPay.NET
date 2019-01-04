using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class TransactionInfo
    {
        [JsonProperty("invoice_timestamp")]
        [JsonConverter(typeof(StringToDateTimeOffsetConverter))]
        public DateTimeOffset InvoiceTimestamp { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("payment_currency")]
        public string PaymentCurrency { get; set; }

        [JsonProperty("payment_txid")]
        public string PaymentTxid { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringToTransactionsStatusConverter))]
        public TransactionStatus Status { get; set; }
    }
}
