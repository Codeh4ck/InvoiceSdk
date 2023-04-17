using System.Drawing;
using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration;

public class FooterConfiguration
{
    public Font Font { get; set; }
    public Color TextColor { get; set; }
    public string Text { get; set; }
}