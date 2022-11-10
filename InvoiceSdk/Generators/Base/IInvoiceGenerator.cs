using InvoiceSdk.Models;

namespace InvoiceSdk.Generators.Base
{
    public interface IInvoiceGenerator
    {
        string GenerateInvoice(Invoice invoice);
        void GenerateInvoice(Invoice invoice, string fullPath, bool format = true);
    }
}
