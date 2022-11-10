using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class AddressConfigurationDefinition
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public AddressConfigurationDefinition(InvoiceConfiguration invoiceConfiguration)
        {
            _invoiceConfiguration = invoiceConfiguration ?? throw new ArgumentNullException(nameof(invoiceConfiguration));
        }

        public AddressConfigurationDefinition WithHeaders(string sellerHeader, string customerHeader)
        {
            if (!string.IsNullOrEmpty(sellerHeader))
                _invoiceConfiguration.AddressConfiguration.SellerHeader = sellerHeader;
            
            if (!string.IsNullOrEmpty(customerHeader))
                _invoiceConfiguration.AddressConfiguration.CustomerHeader = customerHeader;

            return this;
        }

        public ItemTableConfigurationDefinition ThatShowsLabels()
        {
            _invoiceConfiguration.AddressConfiguration.ShowLabels = true;
            return new ItemTableConfigurationDefinition(_invoiceConfiguration);
        }

        public ItemTableConfigurationDefinition ThatDoesNotShowLabels()
        {
            _invoiceConfiguration.AddressConfiguration.ShowLabels = false;
            return new ItemTableConfigurationDefinition(_invoiceConfiguration);
        }
    }
}
