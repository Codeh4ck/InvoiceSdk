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
    }
}
