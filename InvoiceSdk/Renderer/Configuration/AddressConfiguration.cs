namespace InvoiceSdk.Renderer.Configuration;

public class AddressConfiguration
{
    public string SellerHeader { get; set; } = "Seller";
    public string CustomerHeader { get; set; } = "Customer";
    public bool ShowLabels { get; set; } = false;
}