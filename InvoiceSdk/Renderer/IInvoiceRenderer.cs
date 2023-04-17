using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer
{
    public interface IInvoiceRenderer
    {
        IDocument RenderInvoice(Invoice invoice, InvoiceConfiguration configuration);
        Task<IDocument> RenderInvoiceAsync(Invoice invoice, InvoiceConfiguration configuration);
    }
}
