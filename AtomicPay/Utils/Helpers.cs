using AtomicPay.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Text;

namespace AtomicPay.Utils
{
    /// <summary>
    /// internally used helpers
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// gets the current assembly name
        /// </summary>
        internal static string GetAssemblyName()
        {
            return typeof(Helpers).Assembly.GetName().Name;
        }

        /// <summary>
        /// gets the current assembly version
        /// </summary>
        internal static string GetAssemblyVersion()
        {
            var assembly = typeof(Helpers).Assembly.GetName();
            Version version = assembly.Version;

            return version.ToString();
        }

        private static JsonSerializer _jsonSerializer = null;
        private static JsonSerializerSettings _jsonSerializerSettings = null;

        /// <summary>
        /// gets a pre-configured JsonSerializer instance
        /// </summary>
        internal static JsonSerializer GetConfiguredJsonSerializer()
        {
            if (_jsonSerializer == null)
            {
                _jsonSerializer = JsonSerializer.Create(_jsonSerializerSettings);
            }

            return _jsonSerializer;
        }

        /// <summary>
        /// gets a pre-configured JsonSerializerSettings instance
        /// </summary>
        internal static JsonSerializerSettings GetConfiguredJsonSerializerSettings()
        {
            if (_jsonSerializerSettings == null)
            {
                _jsonSerializerSettings = new JsonSerializerSettings()
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters =
                    {
                        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal },
                        StringToResponseStatusConverter.Instance,
                        StringToLongConverter.Instance,
                        StringToDecimalConverter.Instance,
                        StringToIntConverter.Instance,
                        StringToDateTimeOffsetConverter.Instance
                    }
                };
            }
            return _jsonSerializerSettings;
        }

        /// <summary>
        /// create a Base64 representation of the id and key
        /// </summary>
        /// <param name="id">AtomicPay account Id</param>
        /// <param name="key">AtomicPay account Key</param>
        /// <returns></returns>
        internal static string GetBase64AuthHeaderString(string id, string key)
        {
            var buffer = Encoding.UTF8.GetBytes($"{id}:{key}");
            return Convert.ToBase64String(buffer);
        }
    }
}
