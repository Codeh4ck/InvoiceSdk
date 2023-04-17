using System.Drawing;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent;

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

    public LogoConfigurationDefinition WithBackgroundColor(Color color)
    {
        _invoiceConfiguration.HeaderConfiguration.LogoConfiguration.OverrideBackgroundColor = color;
        return this;
    }

    public LogoConfigurationDefinition WithBackgroundColor(string hexColor)
    {
        _invoiceConfiguration.HeaderConfiguration.LogoConfiguration.OverrideBackgroundColor = ColorTranslator.FromHtml(hexColor);
        return this;
    }

    public LogoConfigurationDefinition WithBackgroundColor(int r, int g, int b)
    {
        _invoiceConfiguration.HeaderConfiguration.LogoConfiguration.OverrideBackgroundColor = Color.FromArgb(r, g, g);
        return this;
    }
}