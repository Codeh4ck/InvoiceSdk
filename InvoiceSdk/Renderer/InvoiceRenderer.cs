using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Components;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer;

public class InvoiceRenderer : IInvoiceRenderer
{
    /// <summary>
    /// Renders an invoice document based on the provided Invoice model and configuration.
    /// </summary>
    /// <param name="invoice">The Invoice model to render the document from.</param>
    /// <param name="configuration">The configuration to be passed to the internal invoice renderer.</param>
    /// <returns></returns>
    public IDocument RenderInvoice(Invoice invoice, InvoiceConfiguration configuration) => new InvoiceDocument(invoice, configuration);
    
    /// <summary>
    /// Renders an invoice document based on the provided Invoice model and configuration.
    /// </summary>
    /// <remarks>
    /// This method only wraps the <see cref="RenderInvoice">RenderInvoice()</see> method inside a Task.Run() statement. It is therefore, not truly asynchronous.
    /// </remarks>
    /// <param name="invoice">The Invoice model to render the document from.</param>
    /// <param name="configuration">The configuration to be passed to the internal invoice renderer.</param>
    /// <returns></returns>
    public Task<IDocument> RenderInvoiceAsync(Invoice invoice, InvoiceConfiguration configuration) => Task.Run(() => RenderInvoice(invoice, configuration));
}