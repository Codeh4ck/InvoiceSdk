using System.Drawing;
using QuestPDF.Helpers;
using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration;

public class InvoiceConfiguration
{
    public Font GlobalFont = new("Calibri", 10F);
    public Color GlobalTextColor = ColorTranslator.FromHtml(Colors.Black);

    public HeaderConfiguration HeaderConfiguration = new();
    public FooterConfiguration FooterConfiguration = new();
    public AddressConfiguration AddressConfiguration = new();
    public ItemTableConfiguration ItemTableConfiguration = new();
    public PaymentTableConfiguration PaymentTableConfiguration = new();
}