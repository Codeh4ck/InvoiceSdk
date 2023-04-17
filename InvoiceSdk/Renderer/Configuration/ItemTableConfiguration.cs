using System.Drawing;
using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration;

public class ItemTableConfiguration
{
    public Font TableFont { get; set; }
    public bool DisplayWithoutItems = true;
    public bool DisplayItemDescriptions = true;
    public Color TableHeaderColor = ColorTranslator.FromHtml("#2196f3");
    public string TableHeaderText = "Purchased Items";
    public string NoItemsNotificationHeader = "No items";
    public string NoItemsNotificationText = "Your invoice contains no purchased goods or services.";
}