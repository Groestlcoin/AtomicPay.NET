# AtomicPay.NET
AtomicPay.NET C# SDK enables you to easily interact with your AtomicPay account in your .NET project.

AtomicPay.Net C# SDK is a .NetStandard 2.0 library. This makes it usable in a broad range of project types, like WPF, UWP, Xamarin and more.

### Getting Started

Almost all API calls need to be authenticated. The SDK uses a Config singleton class to make the authentication process as easy as possible, while providing you to possibility to secure your credentials specific to your platform. To initialize the SDK with your credentials, you just need to initialize the Config class:

```
await AtomicPay.Config.Current.Init("YOURACCOUNTID", "YOURACCOUNTPUBLICKEY", "YOURACCOUNTPRIVATEKEY");
```

This will not only initialize the Config, but also verify that you are passing valid credentials. All API calls asks `Config.Current` for credentials internally and throw an Exception if they are missing. 

After initializing the Config, your application is already set up to use `AtomicPayClient`. As `AtomicPayClient` implements `IDisposable`, the easiest way to use the client is:

```
using (var client = new AtomicPayClient())
{
	//read on to see more
}
```

If you prefer a static instance however, just create a static member in your class that uses the SDK and create a new instance of the client.

### Generic response and request types
The SDK uses generic wrappers around the repsonses from the API. These wrappers are repsonsible for (de-)serialization of the json responses/requests. There are two types:

+ `AtomicPayRequest`
+ `AtomicPayResponse`

All methods return an `AtomicPayResponse` object that holds a deserialized representation of the specific entity. If an API call returns an error, the response indicates that there is an error and the error message returned from the API. The `AtomicPayRequest` object wraps the request (for API calls that have a body) objects and serializes them on demand.

### Available API calls
#### Account ([API docs](https://atomicpay.io/api/en#resource-Account))
Get Account info (returns [AccountInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/AccountInfo.cs)):
```
using (var client = new AtomicPayClient())
{
    var account = await client.GetAccountAsync();
}
```

Update Account info (takes [AccountUpdateInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/AccountUpdateInfo.cs), returns [AccountInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/AccountInfo.cs)):
```
using (var client = new AtomicPayClient())
{
   var accUpdate = new AccountUpdateInfo(
      "AccountName",
      "https://msiccdev.net",
      "account@mail.com",
      "USD",
      "BTC",
      TransactionSpeed.High,
      "https://account.notification.url.com");

   var request = new AtomicPayRequest<AccountUpdateInfo>(accUpdate);
   var updatedAccount = await client.UpdateAccountAsync(request);
}
```

#### Billing ([API docs](https://atomicpay.io/api/en#resource-Billing))
Get all bills (returns [BillingList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/BillingList.cs)<[BillInfoShort](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/BillInfoShort.cs)>):
```
using (var client = new AtomicPayClient())
{
   var bills = await client.GetBillsAsync();
}
```
Get specific bill (returns [BillingList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/BillingList.cs)<[BillInfoFull](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/BillinfoFull.cs)>, single entry):
```
using (var client = new AtomicPayClient())
{
   var bill = await client.GetBillByIdAsync("abc123");
}
```

#### Currencies ([API docs](https://atomicpay.io/api/en#resource-Currencies))
Get all currencies [returns [Currencies](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/Currencies.cs) with a List<[CurrencyInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/CurrencyInfo.cs)>):
```
using (var client = new AtomicPayClient())
{
   var allCurrencies = await client.GetCurrenciesAsync();
}
```
Get specific currency [returns [Currencies](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/Currencies.cs) with a List<[CurrencyInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/CurrencyInfo.cs)>, single entry):
```
using (var client = new AtomicPayClient())
{
   var currency = await client.GetCurrencyByNameAsync("USD");
}
```

#### Invoices ([API docs](https://atomicpay.io/api/en#resource-Invoices))
Get Invoices, filtered by time frame and status (returns [InvoicesList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/InvoicesList.cs)<[InvoiceInfoShort](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/InvoiceInfoShort.cs)>)
```
using (var client = new AtomicPayClient())
{
   var today = new DateTimeOffset(DateTime.Today);
   var last30 = new DateTimeOffset(DateTime.Today.Subtract(TimeSpan.FromDays(30)));

   var invoices = await client.GetInvoicesAsync(last30, today, InvoiceStatus.All);
}
```

Get specifc invoice by id (returns [InvoicesList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/InvoicesList.cs)<[InvoiceInfoFull](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/InvoiceInfoFull.cs)>)
```
using (var client = new AtomicPayClient())
{
   var invoice = await client.GetInvoiceByIdAsync("123456");
}
```

Create a new invoice (takes [InvoiceCreationInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/InvoiceCreationInfo.cs), returns [NewInvoiceInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/NewInvoiceInfo.cs))
```
using (var client = new AtomicPayClient())
{
   var request = new InvoiceCreationInfo
      (5,
       "USD",
       123456,
       "invoice for xy",
       TransactionSpeed.Medium,
       "customer@mail.com",
       null,
       null);
    var newInvoiceInfo = new AtomicPayRequest<InvoiceCreationInfo>(request);
    var createdInvoice = await client.CreateInvoiceAsync(newInvoiceInfo);
}
```

#### PayUrl ([API docs](https://atomicpay.io/api/en#resource-PayUrl))
Get all PayUrls, optionally filtered by status (returns [PayUrlList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/PayUrlList.cs)<[PayUrlInfoShort](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/PayUrlInfoShort.cs)>)
```
using (var client = new AtomicPayClient())
{
   var payUrlsList = await client.GetPayUrlsAsync();
}
```

Get specific PayUrl by Id (returns [PayUrlList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/PayUrlList.cs)<[PayUrlInfoFull](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/PayUrlInfoFull.cs)>

#### Rates ([API docs](https://atomicpay.io/api/en#resource-Rates))
Get real time exchange rates for supported currencies [returns [RateInfoList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/RateInfoList.cs)<[RateInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/RateInfo.cs)>):
```
using (var client = new AtomicPayClient())
{
   var rates = await client.GetRatesForCurrencyAsync("USD");
}
```

#### Transactions ([API docs](https://atomicpay.io/api/en#resource-Transactions))
Get transactions, filtered by time frame and status (returns [TransactionList](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/TransactionList.cs)<[TransactionInfo](https://github.com/MSiccDev/AtomicPay.NET/blob/master/AtomicPay/Entity/TransactionInfo.cs)>)
```
using (var client = new AtomicPayClient())
{
   var today = new DateTimeOffset(DateTime.Today);
   var last30 = new DateTimeOffset(DateTime.Today.Subtract(TimeSpan.FromDays(30)));

   var transactions = await client.GetTransactionsAsync(last30, today, TransactionStatus.All);
}
```










