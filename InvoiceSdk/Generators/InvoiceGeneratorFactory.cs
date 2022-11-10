using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Generators
{
    public sealed class InvoiceGeneratorFactory : IInvoiceGeneratorFactory
    {
        public IInvoiceGenerator CreateInvoiceGenerator(InvoiceFormatType invoiceFormatType) =>
            invoiceFormatType switch
            {
                InvoiceFormatType.Xml => new XmlInvoiceGenerator(),
                InvoiceFormatType.Json => new JsonInvoiceGenerator(),
                InvoiceFormatType.Csv => new CsvInvoiceGenerator(),
                _ => throw new ArgumentOutOfRangeException(nameof(invoiceFormatType))
            };
    }
}
