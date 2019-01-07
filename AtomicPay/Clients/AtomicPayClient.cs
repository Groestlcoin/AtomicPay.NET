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

        /// <summary>
        /// Creates a new AtomicPayClient
        /// </summary>
        /// <param name="apiVersion">desired version of the API</param>
        /// <param name="userAgent">optional value for the user agent header</param>
        /// <param name="version">version value for optional user agent header</param>
        /// <param name="throwErrofIfnotAuthenticated">defaults to true, every unauthenticated API call will throw an exception</param>
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
        /// <summary>
        /// Validate the API Keys
        /// </summary>
        /// <param name="accountId">AtomicPay Account Id</param>
        /// <param name="publicKey">AtomicPay Account PublicKey</param>
        /// <param name="privateKey">AtomicPay Account PrivateKey</param>
        /// <returns>authorization result - automatically used for every new instance of AtomicPayClient</returns>
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
        /// <summary>
        /// Get the current authenticated account
        /// </summary>
        /// <returns>AtomicPayResponse with AccountInfo, if successful</returns>
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

        /// <summary>
        /// Updates the current authenticated account
        /// </summary>
        /// <param name="accountInfo">AtomicPayRequest with updated account values</param>
        /// <returns>AtomicPayResponse with updated AccountInfo, if successful</returns>
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
        /// <summary>
        /// Get a list of recent bills
        /// </summary>
        /// <returns>List of BillInfo (short)</returns>
        public async Task<AtomicPayResponse<BillingList<BillInfo>>> GetBillsAsync()
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.GetEp_Billing(_apiVersion)),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<BillingList<BillInfo>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance });
        }

        /// <summary>
        /// Gets details about a bill by Id
        /// </summary>
        /// <param name="billId">desired Bill Id</param>
        /// <returns>Full information about the requested bill</returns>
        public async Task<AtomicPayResponse<BillingList<BillinfoDetails>>> GetBillByIdAsync(string billId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Billing(_apiVersion)}/{billId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<BillingList<BillinfoDetails>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance, StringToPaymentTypeConverter.Instance });
        }
        #endregion

        #region currencies
        /// <summary>
        /// Get FIAT currencies supported by AtomicPay 
        /// </summary>
        /// <returns>List of all supported FIAT currencies</returns>
        public async Task<AtomicPayResponse<Currencies>> GetCurrenciesAsync()
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

        /// <summary>
        /// Get details of specified FIAT currency
        /// </summary>
        /// <param name="currency">ISO 4217 3-character currency code</param>
        /// <returns>details of specified FIAT currency</returns>
        public async Task<AtomicPayResponse<Currencies>> GetCurrencyByNameAsync(string currency)
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
        /// <summary>
        /// Get a list of invoices, filtered by time frame and status
        /// </summary>
        /// <param name="startDate">start date of the time frame</param>
        /// <param name="endDate">end date of the time frame</param>
        /// <param name="invoiceStatus">status of the invoices to fetch</param>
        /// <returns>List of invoices, filtered by time frame and status</returns>
        public async Task<AtomicPayResponse<InvoicesList<InvoiceInfo>>> GetInvoicesAsync(DateTimeOffset startDate, DateTimeOffset endDate, InvoiceStatus invoiceStatus = InvoiceStatus.All)
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

            return new AtomicPayResponse<InvoicesList<InvoiceInfo>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToInvoiceStatusConverter.Instance });
        }

        /// <summary>
        /// Get details of the specified invoice
        /// </summary>
        /// <param name="invoiceId">id of invoice</param>
        /// <returns>Full invoice information of specified invoice</returns>
        public async Task<AtomicPayResponse<InvoicesList<InvoiceInfoDetails>>> GetInvoiceByIdAsync(string invoiceId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_Invoices(_apiVersion)}/{invoiceId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<InvoicesList<InvoiceInfoDetails>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToBillingStatusConverter.Instance, StringToPaymentTypeConverter.Instance });
        }

        /// <summary>
        /// Create a new invoice
        /// </summary>
        /// <param name="newInvoiceRequest">AtomicPayRequest for new invoice</param>
        /// <returns>New Invoice Id & Url</returns>
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
        /// <summary>
        /// Get a list of PayUrls, filtered by status
        /// </summary>
        /// <param name="status">PayUrlStatus to filter</param>
        /// <returns>List of PayUrls, filtered by status</returns>
        public async Task<AtomicPayResponse<PayUrlList<PayUrlInfo>>> GetPayUrlsAsync(PayUrlStatus status = PayUrlStatus.All)
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

            return new AtomicPayResponse<PayUrlList<PayUrlInfo>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance});
        }

        /// <summary>
        /// Get details of specified PayUrl
        /// </summary>
        /// <param name="payUrlId">desired PayUrl Id</param>
        /// <returns>Full information of the specified PayUrl</returns>
        public async Task<AtomicPayResponse<PayUrlList<PayUrlInfoDetails>>> GetPayUrlByIdAsync(string payUrlId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{payUrlId}")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<PayUrlList<PayUrlInfoDetails>>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        /// <summary>
        /// Create a new PayUrl
        /// </summary>
        /// <param name="newPayUrlRequest">AtomicPayRequest with new PayUrl information</param>
        /// <returns>Full information of the created PayUrl</returns>
        public async Task<AtomicPayResponse<UpdatedPayUrlInfo>> CreatePayUrlAsync(AtomicPayRequest<PayUrlUpdateInfo> newPayUrlRequest)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.GetEp_PayUrl(_apiVersion)),
                Content = new StringContent(newPayUrlRequest.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<UpdatedPayUrlInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        /// <summary>
        /// Update existing PayUrl
        /// </summary>
        /// <param name="updateUrlRequest">AtomicPayRequest with updated PayUrl information</param>
        /// <returns>Full information of the updated PayUrl</returns>
        public async Task<AtomicPayResponse<UpdatedPayUrlInfo>> UpdatePayUrlAsync(string urlId, AtomicPayRequest<PayUrlUpdateInfo> updateUrlRequest)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{urlId}"),
                Content = new StringContent(updateUrlRequest.JsonValue, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<UpdatedPayUrlInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }

        /// <summary>
        /// Delete an existing PayUrl
        /// </summary>
        /// <param name="urlId">Id of PayUrl to delete</param>
        /// <returns>Success information of deletion request</returns>
        public async Task<AtomicPayResponse<PayUrlStatusInfo>> DeletePayUrlAsync(string urlId)
        {
            VerifyAuthentication();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Constants.GetEp_PayUrl(_apiVersion)}/{urlId}"),
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);

            return new AtomicPayResponse<PayUrlStatusInfo>(response, false, new List<Newtonsoft.Json.JsonConverter> { StringToPayUrlStatusConverter.Instance, StringToPayUrlExpiryConverter.Instance });
        }


        #endregion

        #region rates
        /// <summary>
        /// Get a list of exchange rates between a FIAT base currency and current authenticated account's supported cryptocurrencies
        /// </summary>
        /// <param name="baseCurrency">ISO 4217 3-character currency code</param>
        /// <returns>list of exchange rates for current authenticated account's supported cryptocurrencies</returns>
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
        /// <summary>
        /// Get a list of transaction for current authenticated account, filtered by timeframe and status
        /// </summary>
        /// <param name="startDate">start date of the time frame</param>
        /// <param name="endDate">end date of the time frame</param>
        /// <param name="transactionStatus">status of the transaction to fetch</param>
        /// <returns>List of transaction for current authenticated account, filtered by timeframe and status</returns>
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
