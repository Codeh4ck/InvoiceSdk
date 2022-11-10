using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class HeaderConfigurationDefinition : ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public HeaderConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
        {
            _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public HeaderConfigurationDefinition WithGlobalFont(Font font)
        {
            _invoiceConfiguration.GlobalFont = font;
            return this;
        }
    }
}
