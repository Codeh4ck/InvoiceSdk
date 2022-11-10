using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class LogoConfigurationDefinition
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public LogoConfigurationDefinition(InvoiceConfiguration invoiceConfiguration)
        {
            _invoiceConfiguration = invoiceConfiguration ?? throw new ArgumentNullException(nameof(invoiceConfiguration));
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
