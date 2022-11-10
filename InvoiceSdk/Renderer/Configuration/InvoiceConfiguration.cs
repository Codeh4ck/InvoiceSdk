using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration
{
    public class InvoiceConfiguration
    {
        public Font GlobalFont = new("Calibri");
        public HeaderConfiguration HeaderConfiguration = new();
        public FooterConfiguration FooterConfiguration = new();
        public AddressConfiguration AddressConfiguration = new();
        public ItemTableConfiguration ItemTableConfiguration = new();
        public PaymentTableConfiguration PaymentTableConfiguration = new();
    }
}
