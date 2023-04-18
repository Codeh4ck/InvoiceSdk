namespace InvoiceSdk.Generators.Base;

public interface IInvoiceGeneratorFactory
{
    IInvoiceGenerator CreateInvoiceGenerator(InvoiceFormatType invoiceFormatType);
}