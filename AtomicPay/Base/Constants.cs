using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Base
{
    public class Constants
    {
        public static string GetApiBaseUrl(string version = "v1") => $"https://merchant.atomicpay.io/api/{version}";

        #region endpoints
        public static string GetEp_Authorization(string version = "v1") => $"{GetApiBaseUrl(version)}/authorization";
        public static string GetEp_Account(string version = "v1") => $"{GetApiBaseUrl(version)}/account";
        public static string GetEp_Billing(string version = "v1") => $"{GetApiBaseUrl(version)}/billing";
        public static string GetEp_Currencies(string version = "v1") => $"{GetApiBaseUrl(version)}/currencies";
        public static string GetEp_Invoices(string version = "v1") => $"{GetApiBaseUrl(version)}/invoices";
        public static string GetEp_PayUrl(string version = "v1") => $"{GetApiBaseUrl(version)}/payurl";
        public static string GetEp_Rates(string version = "v1") => $"{GetApiBaseUrl(version)}/rates";
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
