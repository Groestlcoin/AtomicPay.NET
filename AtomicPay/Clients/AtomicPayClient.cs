using AtomicPay.Base;
using AtomicPay.Entity;
using AtomicPay.JsonConverters;
using AtomicPay.Model;
using AtomicPay.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AtomicPay
{
    public class AtomicPayClient : BaseClient
    {
        private readonly HttpClient _client;
        private readonly string _apiVersion;
        private readonly bool _throwErrofIfnotAuthenticated;

        public AtomicPayClient(string apiVersion = "v1", string userAgent = null, string version = null, bool throwErrofIfnotAuthenticated = true)
        {
            _client = GetClient(null, userAgent, version);
            _apiVersion = apiVersion;
            _throwErrofIfnotAuthenticated = throwErrofIfnotAuthenticated;
        }

        private void VerifyAuthentication()
        {
            if (!Config.Current.IsInitialized)
            {
                if (_throwErrofIfnotAuthenticated)
                    throw new NotSupportedException("Please initialize the client with Config.Current.Init() first.");
            }
            else
            {
                if (_client.DefaultRequestHeaders.Authorization == null)
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Helpers.GetBase64AuthHeaderString(Config.Current.Id, Config.Current.PrivateKey));
            }
        }

        #region authorization
        public async Task<AtomicPayResponse<AuthResult>> AuthorizeAsync(string accountId, string publicKey, string privateKey)
        {
            var authParams = new Dictionary<string, string>
            {
                { Constants.PARAM_AccountId, accountId },
                { Constants.PARAM_Account_PubKey, publicKey },
                { Constants.PARAM_Account_PrivKey, privateKey }
            };

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.GetEp_Authorization(_apiVersion)),
                Content = new StringContent(JsonConvert.SerializeObject(authParams), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<AuthResult>(response, false, null);
        }
        #endregion

        #region account
        public async Task<AtomicPayResponse<AccountInfo>> GetAccountAsync()
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.GetEp_Account(_apiVersion)),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<AccountInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToAccountStatusConverter.Instance, StringToTransactionSpeedConverter.Instance });

        }

        public async Task<AtomicPayResponse<AccountInfo>> UpdateAccountAsync(AtomicPayRequest<AccountUpdateInfo> accountInfo)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(Constants.GetEp_Account(_apiVersion)),
                Content = new StringContent(accountInfo.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<AccountInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToAccountStatusConverter.Instance, StringToTransactionSpeedConverter.Instance });
        }
        #endregion

        #region billing
        public async Task<AtomicPayResponse<BillingList<BillInfoShort>>> GetBillsAsync(string accountId, string privateKey)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.GetEp_Billing(_apiVersion)),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<BillingList<BillInfoShort>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance });
        }

        public async Task<AtomicPayResponse<BillingList<BillinfoFull>>> GetBillAsync(string billId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Billing(_apiVersion)}/{billId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<BillingList<BillinfoFull>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance, StringToPaymentTypeConverter.Instance });
        }
        #endregion

        #region currencies
        public async Task<AtomicPayResponse<Currencies>> GetCurrenciesAsync(string accountId, string privateKey)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.GetEp_Currencies(_apiVersion)),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<Currencies> (response, false, null);
        }

        public async Task<AtomicPayResponse<Currencies>> GetCurrencyByNameAsync(string currency, string accountId, string privateKey)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Currencies(_apiVersion)}/{currency}"),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<Currencies>(response, false, null);
        }

        #endregion

        #region invoices
        public async Task<AtomicPayResponse<InvoicesList<InvoiceInfoShort>>> GetInvoicesAsync(DateTimeOffset startDate, DateTimeOffset endDate, InvoiceStatus invoiceStatus = InvoiceStatus.All)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.GetEp_Invoices(_apiVersion).
                AddParameterToUrl(Constants.PARAM_DateStart, startDate.ToUnixTimeSeconds()).
                AddParameterToUrl(Constants.PARAM_DateEnd, endDate.ToUnixTimeSeconds()).
                AddParameterToUrl(Constants.PARAM_Status, invoiceStatus.ToString().ToLowerInvariant())),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<InvoicesList<InvoiceInfoShort>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToInvoiceStatusConverter.Instance });
        }

        public async Task<AtomicPayResponse<InvoicesList<InvoiceInfoFull>>> GetInvoiceByIdAsync(string invoiceId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Invoices(_apiVersion)}/{invoiceId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<InvoicesList<InvoiceInfoFull>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance, StringToPaymentTypeConverter.Instance });
        }

        public async Task<AtomicPayResponse<NewInvoiceInfo>> CreateInvoiceAsync(AtomicPayRequest<InvoiceCreationInfo> newInvoiceRequest)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.GetEp_Invoices(_apiVersion)),
                Content = new StringContent(newInvoiceRequest.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<NewInvoiceInfo> (response, false, new List<Newtonsoft.Json.JsonConverter> { StringToTransactionSpeedConverter.Instance });
        }

        #endregion

        #region payurl
        public async Task<AtomicPayResponse<PayUrlList<PayUrlInfoShort>>> GetPayUrlsAsync(PayUrlStatus status = PayUrlStatus.All)
        {
            VerifyAuthentication();

            var requestUrl = Constants.GetEp_PayUrl(_apiVersion);
            if (status != PayUrlStatus.All)
                requestUrl.AddParameterToUrl("status", status);

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUrl)
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<PayUrlList<PayUrlInfoShort>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance});
        }

        public async Task<AtomicPayResponse<PayUrlList<PayUrlInfoFull>>> GetPayUrlByIdAsync(string payUrlId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{payUrlId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<PayUrlList<PayUrlInfoFull>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        public async Task<AtomicPayResponse<NewOrUpdatedPayUrlInfo>> CreatePayUrlAsync(AtomicPayRequest<PayUrlCreationOrUpdateInfo> newPayUrlRequest)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.GetEp_PayUrl(_apiVersion)),
                Content = new StringContent(newPayUrlRequest.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<NewOrUpdatedPayUrlInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        public async Task<AtomicPayResponse<NewOrUpdatedPayUrlInfo>> UpdatePayUrlAsync(string urlId, AtomicPayRequest<PayUrlCreationOrUpdateInfo> updateUrlRequest)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{urlId}"),
                Content = new StringContent(updateUrlRequest.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<NewOrUpdatedPayUrlInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        public async Task<AtomicPayResponse<PayUrlDeletionInfo>> DeletePayUrlAsync(string urlId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{urlId}"),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<PayUrlDeletionInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }


        #endregion

        #region rates
        public async Task<AtomicPayResponse<RateInfoList>> GetRatesForCurrencyAsync(string baseCurrency)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Rates(_apiVersion)}/{baseCurrency.ToLowerInvariant()}"),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<RateInfoList>(response, false, null);
        }
        #endregion

        #region transactions
        public async Task<AtomicPayResponse<TransactionList>> GetTransactionsAsync(DateTimeOffset startDate, DateTimeOffset endDate, TransactionStatus transactionStatus = TransactionStatus.All)
        {
            VerifyAuthentication();

            var requestUrl = Constants.GetEp_Transactions(_apiVersion).
                AddParameterToUrl(Constants.PARAM_DateStart, startDate.ToUnixTimeSeconds()).
                AddParameterToUrl(Constants.PARAM_DateEnd, endDate.ToUnixTimeSeconds());

            if (transactionStatus != TransactionStatus.All)
                requestUrl.AddParameterToUrl(Constants.PARAM_Status, transactionStatus.ToString().ToLowerInvariant());

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUrl)
            };   

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<TransactionList>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToTransactionsStatusConverter.Instance });
        }

        #endregion
    }
}
