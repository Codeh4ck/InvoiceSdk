using System.Drawing;

namespace InvoiceSdk.Helpers
{
    internal static class ColorExtensions
    {
        internal static string ToHexString(this Color c) => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}
