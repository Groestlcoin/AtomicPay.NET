using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Model
{
    /// <summary>
    /// Wrapper for change requests to the API
    /// </summary>
    /// <typeparam name="TAtomicPayEntiy">Type of the request</typeparam>
    public class AtomicPayRequest<TAtomicPayEntiy>
    {
        public AtomicPayRequest(TAtomicPayEntiy entity)
        {
            this.Value = entity;
        }

        public TAtomicPayEntiy Value { get; private set; }

        public string JsonValue => JsonConvert.SerializeObject(this.Value);

    }
}
