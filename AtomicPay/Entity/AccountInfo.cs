using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class AccountInfo : EntityBase
    {
        [JsonProperty("account_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("account_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("account_username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("account_website", NullValueHandling = NullValueHandling.Ignore)]
        public string Website { get; set; }

        [JsonProperty("account_email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("account_status", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToAccountStatusConverter))]
        public AccountStatus Status { get; set; }

        [JsonProperty("account_fee", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Fee { get; set; }

        [JsonProperty("account_credits", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToDecimalConverter))]
        public decimal Credits { get; set; }

        [JsonProperty("account_rateLimit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToLongConverter))]
        public long RateLimit { get; set; }

        [JsonProperty("account_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("account_cryptocurrency", NullValueHandling = NullValueHandling.Ignore)]
        public string Cryptocurrency { get; set; }

        [JsonProperty("account_transactionSpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringToTransactionSpeedConverter))]
        public TransactionSpeed TransactionSpeed { get; set; }

        [JsonProperty("account_notificationURL", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationUrl { get; set; }
    }
}
