using System.Drawing;
using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent;

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

    public ItemTableConfigurationDefinition WithHeader(string headerText)
    {
        _invoiceConfiguration.ItemTableConfiguration.TableHeaderText = headerText;
        return this;
    }

    public ItemTableConfigurationDefinition WithHeaderColor(Color color)
    {
        _invoiceConfiguration.ItemTableConfiguration.TableHeaderColor = color;
        return this;
    }

    public ItemTableConfigurationDefinition WithHeaderColor(string hexColor)
    {
        _invoiceConfiguration.ItemTableConfiguration.TableHeaderColor = ColorTranslator.FromHtml(hexColor);
        return this;
    }

    public ItemTableConfigurationDefinition WithHeaderColor(int r, int g, int b)
    {
        _invoiceConfiguration.ItemTableConfiguration.TableHeaderColor = Color.FromArgb(r, g, g);
        return this;
    }
}