using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators
{
    public sealed class CsvInvoiceGenerator : IInvoiceGenerator
    {
        public string GenerateInvoice(Invoice invoice) => CsvSerializer.SerializeToCsv(new[] { invoice });

        public void GenerateInvoice(Invoice invoice, string fullPath, bool format = true)
        {
            string csv = GenerateInvoice(invoice);
            File.WriteAllText(fullPath, csv);
        }
    }
}
