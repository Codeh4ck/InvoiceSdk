using System.Drawing;
using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public class PaymentTableConfigurationDefinition : ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public PaymentTableConfigurationDefinition(InvoiceConfiguration configuration) : base(configuration)
        {
            _invoiceConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public PaymentTableConfigurationDefinition WithFont(Font font)
        {
            _invoiceConfiguration.PaymentTableConfiguration.TableFont = font;
            return this;
        }

        public PaymentTableConfigurationDefinition ThatShowsAlertWithoutItems()
        {
            _invoiceConfiguration.PaymentTableConfiguration.DisplayWithoutItems = true;
            return this;
        }

        public PaymentTableConfigurationDefinition ThatShowsAlertWithoutItems(string header, string text)
        {
            _invoiceConfiguration.PaymentTableConfiguration.DisplayWithoutItems = true;
            _invoiceConfiguration.PaymentTableConfiguration.NoPaymentsNotificationHeader = header;
            _invoiceConfiguration.PaymentTableConfiguration.NoPaymentsNotificationText = text;

            return this;
        }

        public PaymentTableConfigurationDefinition ThatDoesNotShowAlertWithoutItems()
        {
            _invoiceConfiguration.PaymentTableConfiguration.DisplayWithoutItems = false;
            return this;
        }

        public PaymentTableConfigurationDefinition WithHeader(string headerText)
        {
            _invoiceConfiguration.PaymentTableConfiguration.TableHeaderText = headerText;
            return this;
        }

        public PaymentTableConfigurationDefinition WithHeaderColor(Color color)
        {
            _invoiceConfiguration.PaymentTableConfiguration.TableHeaderColor = color;
            return this;
        }

        public PaymentTableConfigurationDefinition WithHeaderColor(string hexColor)
        {
            _invoiceConfiguration.PaymentTableConfiguration.TableHeaderColor = ColorTranslator.FromHtml(hexColor);
            return this;
        }

        public PaymentTableConfigurationDefinition WithHeaderColor(int r, int g, int b)
        {
            _invoiceConfiguration.PaymentTableConfiguration.TableHeaderColor = Color.FromArgb(r, g, g);
            return this;
        }
    }
}
