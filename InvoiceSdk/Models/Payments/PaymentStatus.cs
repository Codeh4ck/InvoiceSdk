namespace InvoiceSdk.Models.Payments
{
    [Flags]
    public enum PaymentStatus
    {
        Paid = 1,
        Completed = 2,
        Reverted = 4,
        Cancelled = 8,
        Rejected = 16
    }
}
