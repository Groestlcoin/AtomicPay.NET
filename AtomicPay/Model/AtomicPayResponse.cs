using AtomicPay.Entity;
using AtomicPay.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AtomicPay.Model
{
    /// <summary>
    /// Wrapper around the response of the API
    /// </summary>
    /// <typeparam name="TAtomicPayEntity">Entity Type of the response</typeparam>
    public class AtomicPayResponse<TAtomicPayEntity> where TAtomicPayEntity : EntityBase
    {
        private JsonSerializerSettings _jsonSerializerSettings;
        private JsonSerializer _jsonSerializer;

        public AtomicPayResponse(bool throwSerializationExceptions)
        {
            _jsonSerializerSettings = Helpers.GetConfiguredJsonSerializerSettings();
            _jsonSerializer = Helpers.GetConfiguredJsonSerializer();

            if (!throwSerializationExceptions)
            {
                _jsonSerializerSettings.Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    this.Value.Message = args.ErrorContext.Error.Message;
                    this.Value.Code = ResponseStatus.NotValid;

                    this.Value = default;

                    args.ErrorContext.Handled = true;
                };

            }
        }

        public AtomicPayResponse(HttpResponseMessage response, bool throwSerializationExceptions = false, List<JsonConverter> converters = null) : this(throwSerializationExceptions)
        {
            if (converters?.Any() ?? false)
                foreach (var converter in converters)
                    _jsonSerializer.Converters.Add(converter);

            //reading stream to boost performance
            using (var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        this.Value = _jsonSerializer.Deserialize<TAtomicPayEntity>(jsonReader);
                    }
                }
            }
        }

        public TAtomicPayEntity Value { get; private set; }

        public string JsonValue => JsonConvert.SerializeObject(this.Value);

        public bool IsError => this.Value.Code != ResponseStatus.OK;
    }
}
