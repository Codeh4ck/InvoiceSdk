using InvoiceSdk.Models;
using InvoiceSdk.Generators;
using InvoiceSdk.Generators.Base;

namespace InvoiceSdk.Helpers;

public static class InvoiceExtensions
{
    public static IInvoiceGeneratorFactory InvoiceGeneratorFactoryImpl = new InvoiceGeneratorFactory();

    public static string ToXmlInvoice(this Invoice invoice)
    {
        IInvoiceGenerator generator = InvoiceGeneratorFactoryImpl.CreateInvoiceGenerator(InvoiceFormatType.Xml);
        return generator.GenerateInvoice(invoice);
    }

    public static string ToJsonInvoice(this Invoice invoice)
    {
        IInvoiceGenerator generator = InvoiceGeneratorFactoryImpl.CreateInvoiceGenerator(InvoiceFormatType.Json);
        return generator.GenerateInvoice(invoice);
    }

    public static string ToCsvInvoice(this Invoice invoice)
    {
        IInvoiceGenerator generator = InvoiceGeneratorFactoryImpl.CreateInvoiceGenerator(InvoiceFormatType.Csv);
        return generator.GenerateInvoice(invoice);
    }
}