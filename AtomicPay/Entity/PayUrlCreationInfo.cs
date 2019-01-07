namespace AtomicPay.Entity
{
    public class PayUrlCreationInfo : PayUrlUpdateInfo
    {
        public PayUrlCreationInfo(string name, string currency, TransactionSpeed transactionSpeed = TransactionSpeed.Medium, PayUrlExpiry urlExpiry = PayUrlExpiry.None, long? orderId = null, decimal? orderprice = null, string description = null, string notificationemail = null, string notificationUrl = null, string redirecturl = null) : 
			base(name, currency, transactionSpeed,urlExpiry,orderId,orderprice,description,notificationemail,notificationUrl,redirecturl)
        {

        }
    }
}