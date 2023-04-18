using System.Drawing;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent;

public class HeaderConfigurationDefinition : ConfigurationDefinitionBase
{
    private readonly InvoiceConfiguration _invoiceConfiguration;

    public HeaderConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
    {
        _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public HeaderConfigurationDefinition WithTextColor(Color color)
    {
        _invoiceConfiguration.HeaderConfiguration.TextColor = color;
        return this;
    }

    public HeaderConfigurationDefinition WithTextColor(string hexColor)
    {
        _invoiceConfiguration.HeaderConfiguration.TextColor = ColorTranslator.FromHtml(hexColor);
        return this;
    }

    public HeaderConfigurationDefinition WithTextColor(int r, int g, int b)
    {
        _invoiceConfiguration.HeaderConfiguration.TextColor = Color.FromArgb(r, g, g);
        return this;
    }
}