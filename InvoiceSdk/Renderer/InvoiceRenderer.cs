using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Components;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer
{
    public class InvoiceRenderer : IInvoiceRenderer
    {
        public IDocument RenderInvoice(Invoice invoice, InvoiceConfiguration configuration)
        {
            return new InvoiceDocument(invoice, configuration);
        }
    }
}
