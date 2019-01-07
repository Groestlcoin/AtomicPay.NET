using AtomicPay.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AtomicPay.Base
{
    public abstract class BaseClient : IDisposable
    {
        private static HttpClient _httpClientInstance;

        /// <summary>
        /// static base implementation of configured HttpClient
        /// </summary>
        /// <param name="modifiedSince">value for if-modified=since header</param>
        /// <param name="userAgent">value for user agent header</param>
        /// <param name="version">value for version in user agent header</param>
        /// <param name="allowCaching">value to control caching behavior</param>
        internal static HttpClient GetClient(DateTime? modifiedSince = null, string userAgent = null, string version = null, bool allowCaching = false)
        {
            if (_httpClientInstance == null)
            {
                //making sure we are able to decompress using default methods
                var handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                _httpClientInstance = new HttpClient(handler);

                if (modifiedSince.HasValue)
                {
                    _httpClientInstance.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(modifiedSince.Value);
                }

                _httpClientInstance.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = allowCaching };
                _httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClientInstance.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));

                _httpClientInstance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                _httpClientInstance.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                _httpClientInstance.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(string.IsNullOrEmpty(userAgent) ? Helpers.GetAssemblyName() : userAgent,
                                                                                   string.IsNullOrEmpty(version) ? Helpers.GetAssemblyVersion() : version));

                //stoping round trips
                _httpClientInstance.DefaultRequestHeaders.ExpectContinue = false;
            }

            return _httpClientInstance;
        }

        public void Dispose()
        {
            _httpClientInstance = null;
        }
    }
}
