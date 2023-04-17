using InvoiceSdk.Models;

namespace InvoiceSdk.Generators.Base
{
    public interface IInvoiceGenerator
    {
        string GenerateInvoice(Invoice invoice);
        Invoice GenerateInvoice(string serialized);
        Task GenerateInvoice(Invoice invoice, string fullPath, bool format = true, CancellationToken token = default);
    }
}
