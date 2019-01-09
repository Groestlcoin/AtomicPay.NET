using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicPay.Entity
{
    public enum ResponseStatus
    {
        OK = 200,
        NotValid = 400,
        NotAuthorized = 401,
        NotFound = 404
    }

    public enum TransactionSpeed
    {
        Low,
        Medium,
        High
    }

    public enum AccountStatus
    {
        Pending,
        Active,
        Locked,
        Suspended,
        Banned
    }

    public enum BillingStatus
    {
        All,
        Pending,
        Paid,
        Overdue
    }

    public enum PaymentType
    {
        Default,
        AtomicPay
    }

    public enum InvoiceStatus
    {
        All,
        New,
        Paid,
        Confirmed,
        Complete,
        Expired,
        Invalid,
        Underpaid,
        Overpaid,
        PaidAfterExpiry
    }

    public enum InvoiceStatusException
    {
        None,
        Underpaid,
        Overpaid,
        Paid_After_Expiry
    }

    public enum PayUrlStatus
    {
        All,
        Active,
        Deleted,
        Expired
    }

    public enum PayUrlExpiry
    {
        None,
        OneDay,
        SevenDays,
        FifteenDays,
        ThirdyDays
    }

    public enum TransactionStatus
    {
        New,
        Paid,
        Confirmed,
        Complete,
        Expired,
        Paid_After_Expiry,
        All
    }
}
