using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public abstract class ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _configuration;

        protected ConfigurationDefinitionBase(InvoiceConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public HeaderConfigurationDefinition ConfigureHeader() => new(_configuration);
        public LogoConfigurationDefinition ConfigureLogo() => new(_configuration);
        public AddressConfigurationDefinition ConfigureAddress() => new(_configuration);
        public ItemTableConfigurationDefinition ConfigureItemTable() => new(_configuration);
        public PaymentTableConfigurationDefinition ConfigurePaymentTable() => new(_configuration);
        public FooterConfigurationDefinition ConfigureFooter() => new(_configuration);

        public InvoiceConfiguration Build() => _configuration;
    }
}
