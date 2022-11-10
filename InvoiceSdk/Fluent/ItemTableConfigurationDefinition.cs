using InvoiceSdk.Renderer.Configuration;
using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Fluent
{
    public class ItemTableConfigurationDefinition : ConfigurationDefinitionBase
    {
        private readonly InvoiceConfiguration _invoiceConfiguration;

        public ItemTableConfigurationDefinition(InvoiceConfiguration invoiceConfiguration) : base(invoiceConfiguration)
        {
            _invoiceConfiguration = invoiceConfiguration ?? throw new ArgumentNullException(nameof(invoiceConfiguration));
            _invoiceConfiguration.ItemTableConfiguration.TableFont = _invoiceConfiguration.GlobalFont;
        }

        public ItemTableConfigurationDefinition WithFont(Font font)
        {
            _invoiceConfiguration.ItemTableConfiguration.TableFont = font;
            return this;
        }

        public ItemTableConfigurationDefinition ThatShowsAlertWithoutItems()
        {
            _invoiceConfiguration.ItemTableConfiguration.DisplayWithoutItems = true;
            return this;
        }

        public ItemTableConfigurationDefinition ThatShowsAlertWithoutItems(string header, string text)
        {
            _invoiceConfiguration.ItemTableConfiguration.DisplayWithoutItems = true;
            _invoiceConfiguration.ItemTableConfiguration.NoItemsNotificationHeader = header;
            _invoiceConfiguration.ItemTableConfiguration.NoItemsNotificationText = text;

            return this;
        }

        public ItemTableConfigurationDefinition ThatDoesNotShowAlertWithoutItems()
        {
            _invoiceConfiguration.ItemTableConfiguration.DisplayWithoutItems = false;
            return this;
        }

        public ItemTableConfigurationDefinition ThatDisplaysItemDescriptions()
        {
            _invoiceConfiguration.ItemTableConfiguration.DisplayItemDescriptions = true;
            return this;
        }

        public ItemTableConfigurationDefinition ThatDoesNotDisplayItemDescriptions()
        {
            _invoiceConfiguration.ItemTableConfiguration.DisplayItemDescriptions = false;
            return this;
        }
    }
}
