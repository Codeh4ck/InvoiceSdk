using System.Drawing;
using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent;

public class InvoiceConfigurationDefinition : ConfigurationDefinitionBase
{
    private readonly InvoiceConfiguration _invoiceConfiguration;

    public InvoiceConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
    {
        _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public InvoiceConfigurationDefinition WithGlobalFont(Font font)
    {
        _invoiceConfiguration.GlobalFont = font;
        return this;
    }

    public InvoiceConfigurationDefinition WithGlobalTextColor(Color color)
    {
        _invoiceConfiguration.GlobalTextColor = color;
        return this;
    }

    public InvoiceConfigurationDefinition WithGlobalTextColor(string hexColor)
    {
        _invoiceConfiguration.GlobalTextColor = ColorTranslator.FromHtml(hexColor);
        return this;
    }

    public InvoiceConfigurationDefinition WithGlobalTextColor(int r, int g, int b)
    {
        _invoiceConfiguration.GlobalTextColor = Color.FromArgb(r, g, g);
        return this;
    }
}