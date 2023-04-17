using InvoiceSdk.Models.Payments;

namespace InvoiceSdk.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Subject { get; set; }
    public string Note { get; set; }
    public InvoiceCurrencySymbol InvoiceCurrency { get; set; }
    public Address CustomerAddress { get; set; }
    public Address SellerAddress { get; set; }
    public List<InvoiceItem> Items { get; set; }
    public List<Payment>  Payments { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime DueAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public void MarkAs(InvoiceStatus status)
    {
        Status = status;
    }
}