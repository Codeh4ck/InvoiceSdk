using System.Drawing;

namespace InvoiceSdk.Renderer.Internal;

public struct Font
{
    public string Name { get; set; }
    public float Size { get; set; }
    public bool Bold { get; set; }
    public bool Italics { get; set; }
    public Color Color { get; set; }

    public Font(string name, float size = 8f, bool bold = false, bool italics = false)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

        Name = name;
        Size = size;
        Bold = bold;
        Italics = italics;

        Color = Color.Black;
    }

    public Font(string name, Color color, float size = 8f, bool bold = false, bool italics = false) : this(name, size, bold, italics)
    {
        Color = color;
    }
}