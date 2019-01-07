using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Base
{
    public class Constants
    {
        /// <summary>
        /// Get the API base url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API base url with version</returns>
        public static string GetApiBaseUrl(string version = "v1") => $"https://merchant.atomicpay.io/api/{version}";

        #region endpoints
        /// <summary>
        /// Get the API authorization endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API authorization endpoint url with version</returns>
        public static string GetEp_Authorization(string version = "v1") => $"{GetApiBaseUrl(version)}/authorization";

        /// <summary>
        /// Get the API account endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API account endpoint url with version</returns>
        public static string GetEp_Account(string version = "v1") => $"{GetApiBaseUrl(version)}/account";

        /// <summary>
        /// Get the API billing endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API billing endpoint url with version</returns>
        public static string GetEp_Billing(string version = "v1") => $"{GetApiBaseUrl(version)}/billing";

        /// <summary>
        /// Get the API currencies endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API currencies endpoint url with version</returns>
        public static string GetEp_Currencies(string version = "v1") => $"{GetApiBaseUrl(version)}/currencies";

        /// <summary>
        /// Get the API invoices endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API invoices endpoint url with version</returns>
        public static string GetEp_Invoices(string version = "v1") => $"{GetApiBaseUrl(version)}/invoices";

        /// <summary>
        /// Get the API pay url endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API pay url endpoint url with version</returns>
        public static string GetEp_PayUrl(string version = "v1") => $"{GetApiBaseUrl(version)}/payurl";

        /// <summary>
        /// Get the API rates endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API rates endpoint url with version</returns>
        public static string GetEp_Rates(string version = "v1") => $"{GetApiBaseUrl(version)}/rates";

        /// <summary>
        /// Get the API transactions endpoint url
        /// </summary>
        /// <param name="version">API version</param>
        /// <returns>API transactions endpoint url with version</returns>
        public static string GetEp_Transactions(string version = "v1") => $"{GetApiBaseUrl(version)}/transactions";
        #endregion


        #region parameters
        public const string PARAM_AccountId = "account_id";
        public const string PARAM_Account_PubKey = "account_publicKey";
        public const string PARAM_Account_PrivKey = "account_privateKey";
        public const string PARAM_DateStart = "dateStart";
        public const string PARAM_DateEnd = "dateEnd";
        public const string PARAM_Status = "status";


        #endregion

    }
}
