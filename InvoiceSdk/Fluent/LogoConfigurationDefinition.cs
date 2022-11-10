using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class LogoConfigurationDefinition : ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public LogoConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
        {
            _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            }

        public LogoConfigurationDefinition WithLogoHeightCm(float logoHeight)
        {
            _invoiceConfiguration.HeaderConfiguration.LogoConfiguration.LogoHeightCm = logoHeight;
            return this;
        }

        public LogoConfigurationDefinition WithLogoFile(string logoFile)
        {
            _invoiceConfiguration.HeaderConfiguration.LogoConfiguration.LogoSourceFile = logoFile;
            return this;
        }
    }
}
