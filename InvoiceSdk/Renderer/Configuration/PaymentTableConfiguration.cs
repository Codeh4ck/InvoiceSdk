using System.Drawing;
using InvoiceSdk.Renderer.Internal;

namespace InvoiceSdk.Renderer.Configuration
{
    public class PaymentTableConfiguration
    {
        public Font TableFont { get; set; }
        public bool DisplayWithoutItems = true;
        public Color TableHeaderColor = ColorTranslator.FromHtml("#2196f3");
        public string TableHeaderText = "Payments";
        public string NoPaymentsNotificationHeader = "No payments";
        public string NoPaymentsNotificationText = "You haven't made any payment yet to fullfil this invoice.";
    }
}
