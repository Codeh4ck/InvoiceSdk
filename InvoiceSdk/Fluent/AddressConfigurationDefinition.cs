using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent;

public class AddressConfigurationDefinition : ConfigurationDefinitionBase
{
    private readonly InvoiceConfiguration _invoiceConfiguration;

    public AddressConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
    {
        _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public AddressConfigurationDefinition WithHeaders(string sellerHeader, string customerHeader)
    {
        if (!string.IsNullOrEmpty(sellerHeader))
            _invoiceConfiguration.AddressConfiguration.SellerHeader = sellerHeader;
            
        if (!string.IsNullOrEmpty(customerHeader))
            _invoiceConfiguration.AddressConfiguration.CustomerHeader = customerHeader;

        return this;
    }

    public AddressConfigurationDefinition ThatShowsLabels()
    {
        _invoiceConfiguration.AddressConfiguration.ShowLabels = true;
        return this;
    }

    public AddressConfigurationDefinition ThatDoesNotShowLabels()
    {
        _invoiceConfiguration.AddressConfiguration.ShowLabels = false;
        return this;
    }
}