using System.Drawing;
using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class FooterConfigurationDefinition : ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public FooterConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
        {
            _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _invoiceConfiguration.FooterConfiguration.Font = _invoiceConfiguration.GlobalFont;
        }

        public FooterConfigurationDefinition WithFont(Font font)
        {
            _invoiceConfiguration.FooterConfiguration.Font = font;
            return this;
        }

        public FooterConfigurationDefinition WithText(string text)
        {
            _invoiceConfiguration.FooterConfiguration.Text = text;
            return this;
        }

        public FooterConfigurationDefinition WithTextColor(Color color)
        {
            _invoiceConfiguration.FooterConfiguration.TextColor = color;
            return this;
        }

        public FooterConfigurationDefinition WithTextColor(string hexColor)
        {
            _invoiceConfiguration.FooterConfiguration.TextColor = ColorTranslator.FromHtml(hexColor);
            return this;
        }

        public FooterConfigurationDefinition WithTextColor(int r, int g, int b)
        {
            _invoiceConfiguration.FooterConfiguration.TextColor = Color.FromArgb(r, g, g);
            return this;
        }
    }
}
