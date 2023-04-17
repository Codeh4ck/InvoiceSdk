using System.Text;
using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators;

public sealed class JsonInvoiceGenerator : IInvoiceGenerator
{
    public string GenerateInvoice(Invoice invoice)
    {
        JsonSerializer<Invoice> serializer = new JsonSerializer<Invoice>();
        return serializer.SerializeToString(invoice);
    }

    public Invoice GenerateInvoice(string serialized)
    {
        JsonSerializer<Invoice> serializer = new JsonSerializer<Invoice>();
        return serializer.DeserializeFromString(serialized);
    }

    public async Task GenerateInvoice(Invoice invoice, string fullPath, bool format = true, CancellationToken token = default)
    {
        string json = GenerateInvoice(invoice);
        if (format) json = json.IndentJson();

        await File.WriteAllTextAsync(fullPath, json, Encoding.UTF8, token);
    }
}