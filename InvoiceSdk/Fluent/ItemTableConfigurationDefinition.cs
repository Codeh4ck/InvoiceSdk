using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class ItemTableConfigurationDefinition
    {
        private InvoiceConfiguration _invoiceConfiguration;

        public ItemTableConfigurationDefinition(InvoiceConfiguration invoiceConfiguration)
        {
            _invoiceConfiguration = invoiceConfiguration ?? throw new ArgumentNullException(nameof(invoiceConfiguration));
        }
    }
}
