using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class HeaderConfigurationDefinition
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public HeaderConfigurationDefinition(InvoiceConfiguration invoiceConfiguration)
        {
            _invoiceConfiguration = invoiceConfiguration ?? throw new ArgumentNullException(nameof(invoiceConfiguration));
        }

        public LogoConfigurationDefinition WithGlobalFont(Font font)
        {
            _invoiceConfiguration.GlobalFont = font;
            return new LogoConfigurationDefinition(_invoiceConfiguration);
        }
    }
}
