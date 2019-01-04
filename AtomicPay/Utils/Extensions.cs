using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Utils
{
    public static class Extensions
    {
        public static string GetJsonDataBody(this Dictionary<string, string> parameters)
        {
            return JsonConvert.SerializeObject(parameters);
        }

        public static string AddParameterToUrl(this string url, string parameterName, object parameterValue)
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
