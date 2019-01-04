using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public class AccountUpdateInfo
    {
        public AccountUpdateInfo(string name = null, string website = null, string mail = null, string currency = null, string cryptocurrency = null, TransactionSpeed transactionSpeed = default, string notificationUrl = null)
        {
            this.Name = name;
            this.Website = website;
            this.Email = mail;
            this.Currency = currency;
            this.Cryptocurrency = cryptocurrency;
            this.TransactionSpeed = transactionSpeed;
            this.NotificationUrl = notificationUrl;
        }


        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("account_website")]
        public string Website { get; set; }

        [JsonProperty("account_email")]
        public string Email { get; set; }

        [JsonProperty("account_currency")]
        public string Currency { get; set; }

        [JsonProperty("account_cryptocurrency")]
        public string Cryptocurrency { get; set; }

        [JsonProperty("account_transactionSpeed")]
        [JsonConverter(typeof(StringToTransactionSpeedConverter))]
        public TransactionSpeed TransactionSpeed { get; set; }

        [JsonProperty("account_notificationURL")]
        public string NotificationUrl { get; set; }
    }
}
