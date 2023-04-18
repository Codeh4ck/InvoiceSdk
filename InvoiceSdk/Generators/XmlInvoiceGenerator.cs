using System.Text;
using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators;

public sealed class XmlInvoiceGenerator : IInvoiceGenerator
{
    public XmlInvoiceGenerator()
    {
        XmlSerializer.XmlWriterSettings.Encoding = Encoding.UTF8;
        XmlSerializer.XmlWriterSettings.OmitXmlDeclaration = false;
    }

    public string GenerateInvoice(Invoice invoice) => XmlSerializer.SerializeToString(invoice);
    public Invoice GenerateInvoice(string serialized) => XmlSerializer.DeserializeFromString<Invoice>(serialized);

    public void GenerateInvoice(Invoice invoice, string fullPath, bool format = true)
    {
        XmlSerializer.XmlWriterSettings.Indent = format;
        string xml = GenerateInvoice(invoice);

        File.WriteAllText(fullPath, xml, Encoding.UTF8);
    }

    public async Task GenerateInvoiceAsync(Invoice invoice, string fullPath, bool format = true,
        CancellationToken token = default)
    {
        XmlSerializer.XmlWriterSettings.Indent = format;
        string xml = GenerateInvoice(invoice);

        await File.WriteAllTextAsync(fullPath, xml, Encoding.UTF8, token);
    }
}