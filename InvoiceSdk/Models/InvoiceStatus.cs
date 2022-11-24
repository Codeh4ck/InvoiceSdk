namespace InvoiceSdk.Models
{
    [Flags]
    public enum InvoiceStatus
    {
        Issued = 1,
        Pending = 2,
        Paid = 4,
        Completed = 8,
        Cancelled = 16
    }
}
