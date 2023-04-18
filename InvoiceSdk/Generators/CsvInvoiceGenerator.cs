using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators;

public sealed class CsvInvoiceGenerator : IInvoiceGenerator
{
    public string GenerateInvoice(Invoice invoice) => CsvSerializer.SerializeToCsv(new[] { invoice });
    public Invoice GenerateInvoice(string serialized) => CsvSerializer.DeserializeFromString<Invoice>(serialized);
    
    public void GenerateInvoice(Invoice invoice, string fullPath, bool format)
    {
        string csv = GenerateInvoice(invoice);
        File.WriteAllText(fullPath, csv);
    }

    public async Task GenerateInvoiceAsync(Invoice invoice, string fullPath, bool format = true, CancellationToken token = default)
    {
        string csv = GenerateInvoice(invoice);
        await File.WriteAllTextAsync(fullPath, csv, token);
    }
}