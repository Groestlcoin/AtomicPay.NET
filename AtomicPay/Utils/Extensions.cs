using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// add a parameter to an url string
        /// </summary>
        /// <param name="url">url string</param>
        /// <param name="parameterName">name of parameter to add</param>
        /// <param name="parameterValue">value of parameter to add</param>
        /// <returns></returns>
        internal static string AddParameterToUrl(this string url, string parameterName, object parameterValue)
        {
            if (url.Contains("?"))
            {
                return $"{url}&{parameterName}={parameterValue.ToString()}";
            }
            else
            {
                return $"{url}?{parameterName}={parameterValue.ToString()}";
            }
        }

    }
}
