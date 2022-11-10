using System.Text;
using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators
{
    public sealed class XmlInvoiceGenerator : IInvoiceGenerator
    {
        public string GenerateInvoice(Invoice invoice) => XmlSerializer.SerializeToString(invoice);

        public void GenerateInvoice(Invoice invoice, string fullPath, bool format = true)
        {
            XmlSerializer.XmlWriterSettings.Encoding = Encoding.UTF8;
            XmlSerializer.XmlWriterSettings.Indent = format;
            XmlSerializer.XmlWriterSettings.OmitXmlDeclaration = false;

            string xml = GenerateInvoice(invoice);

            File.WriteAllText(fullPath, xml, Encoding.UTF8);
        }
    }
}
