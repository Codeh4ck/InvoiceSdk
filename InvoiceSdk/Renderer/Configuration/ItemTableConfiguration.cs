using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration
{
    public class ItemTableConfiguration
    {
        public Font TableFont { get; set; }
        public bool DisplayWithoutItems = true;
        public bool DisplayItemDescriptions = true;
        public string NoItemsNotificationHeader = "No items";
        public string NoItemsNotificationText = "Your invoice contains no purchased goods or services.";
    }
}
