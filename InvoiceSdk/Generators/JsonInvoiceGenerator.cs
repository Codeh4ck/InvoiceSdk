using System.Text;
using ServiceStack.Text;
using InvoiceSdk.Models;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators
{
    public sealed class JsonInvoiceGenerator : IInvoiceGenerator
    {
        public string GenerateInvoice(Invoice invoice)
        {
            JsonSerializer<Invoice> serializer = new JsonSerializer<Invoice>();
            return serializer.SerializeToString(invoice);
        }

        public void GenerateInvoice(Invoice invoice, string fullPath, bool format = true)
        {
            string json = GenerateInvoice(invoice);
            if (format) json = json.IndentJson();

            File.WriteAllText(fullPath, json, Encoding.UTF8);
        }
    }
}
