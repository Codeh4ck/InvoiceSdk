using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration
{
    public class InvoiceConfiguration
    {
        public Font GlobalFont { get; set; }
        public HeaderConfiguration HeaderConfiguration = new();
        public FooterConfiguration FooterConfiguration { get; set; }
        public AddressConfiguration AddressConfiguration = new();
        public ItemTableConfiguration ItemTableConfiguration = new();
        public PaymentTableConfiguration PaymentTableConfiguration = new();
    }
}
