using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtomicPay
{
    public class Config : IDisposable
    {
        private static Config _instance;

        /// <summary>
        /// current instance of AtomicPay configuration
        /// </summary>
        public static Config Current => _instance ?? (_instance = new Config());

        /// <summary>
        /// initializes the current Config instance and validates the API keys
        /// </summary>
        /// <param name="id">AtomicPay Account Id</param>
        /// <param name="publicKey">AtomicPay Account PublicKey</param>
        /// <param name="privateKey">AtomicPay Account PrivateKey</param>
        /// <param name="throwErrorIfInvalid">if set to true, will throw an exception on invalid API keys</param>
        public async Task Init(string id, string publicKey, string privateKey, bool throwErrorIfInvalid = false)
        {
            this.Id = id;
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;

            if (!string.IsNullOrWhiteSpace(this.Id) && !string.IsNullOrWhiteSpace(this.PublicKey) && !string.IsNullOrWhiteSpace(this.PrivateKey))
            {
                using (var client = new AtomicPayClient())
                {
                    var auth = await client.AuthorizeAsync(this.Id, this.PublicKey, this.PrivateKey).ConfigureAwait(false);
                    if (!auth.IsError)
                    {
                        if (auth.Value.Code == Entity.ResponseStatus.OK)
                            this.IsInitialized = true;
                        else
                        {
                            if (throwErrorIfInvalid)
                            {
                                throw new ArgumentException(auth.Value.Message);
                            }
                            else
                            {
                                this.IsInitialized = false;
                                this.Id = null;
                                this.PublicKey = null;
                                this.PrivateKey = null;
                            }
                        }
                    }

                }
            }
        }

        public void Dispose()
        {
            _instance = null;
        }

        public string Id { get; private set; }

        public string PublicKey { get; private set; }

        public string PrivateKey { get; private set; }

        public bool IsInitialized { get; private set; }
    }
}
