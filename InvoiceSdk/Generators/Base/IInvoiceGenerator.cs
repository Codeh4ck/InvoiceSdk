using InvoiceSdk.Models;

namespace InvoiceSdk.Generators.Base;

public interface IInvoiceGenerator
{
    string GenerateInvoice(Invoice invoice);
    Invoice GenerateInvoice(string serialized);
    void GenerateInvoice(Invoice invoice, string fullPath, bool format = true);
    Task GenerateInvoiceAsync(Invoice invoice, string fullPath, bool format = true, CancellationToken token = default);
}