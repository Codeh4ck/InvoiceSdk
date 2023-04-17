using System.Text;
using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators;

public sealed class XmlInvoiceGenerator : IInvoiceGenerator
{
    public string GenerateInvoice(Invoice invoice) => XmlSerializer.SerializeToString(invoice);
    public Invoice GenerateInvoice(string serialized) => XmlSerializer.DeserializeFromString<Invoice>(serialized);

    public async Task GenerateInvoice(Invoice invoice, string fullPath, bool format = true, CancellationToken token = default)
    {
        XmlSerializer.XmlWriterSettings.Encoding = Encoding.UTF8;
        XmlSerializer.XmlWriterSettings.Indent = format;
        XmlSerializer.XmlWriterSettings.OmitXmlDeclaration = false;

        string xml = GenerateInvoice(invoice);

        await File.WriteAllTextAsync(fullPath, xml, Encoding.UTF8, token);
    }
}