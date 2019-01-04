using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class BillinfoFull : BillInfoShort
    {
        [JsonProperty("bill_days")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int BillDays { get; set; }

        [JsonProperty("bill_credits")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal BillCredits { get; set; }

        [JsonProperty("tx_total")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int TxTotal { get; set; }

        [JsonProperty("tx_volume")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal TxVolume { get; set; }

        [JsonProperty("tx_fee")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal TxFee { get; set; }

        [JsonProperty("amount_due")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal AmountDue { get; set; }

        [JsonProperty("payment_timestamp")]
        [JsonConverter(typeof(StringToDateTimeOffsetConverter))]
        public DateTimeOffset PaymentTimestamp { get; set; }

        [JsonProperty("payment_type")]
        [JsonConverter(typeof(StringToPaymentTypeConverter))]
        public PaymentType PaymentType { get; set; }

        [JsonProperty("payment_price")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentPrice { get; set; }

        [JsonProperty("payment_currency")]
        public string PaymentCurrency { get; set; }

        [JsonProperty("payment_rate")]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal PaymentRate { get; set; }

        [JsonProperty("payment_address")]
        public string PaymentAddress { get; set; }

    }
}
