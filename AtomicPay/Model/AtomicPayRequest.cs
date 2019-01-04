using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Model
{
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
