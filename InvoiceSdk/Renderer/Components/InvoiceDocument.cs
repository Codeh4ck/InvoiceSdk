using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using InvoiceSdk.Helpers;
using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Renderer.Components
{
    public class InvoiceDocument : IDocument
    {
        private readonly Invoice _invoice;
        private readonly InvoiceConfiguration _configuration;

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public InvoiceDocument(Invoice invoice, InvoiceConfiguration configuration)
        {
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.DefaultTextStyle(style => style.FontSize(_configuration.GlobalFont.Size));
                    page.DefaultTextStyle(style => style.FontFamily(_configuration.GlobalFont.Name));

                    page.Margin(50);

                    page.Header().Component(new HeaderComponent(_invoice, _configuration.HeaderConfiguration));
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();

                        if (_configuration.FooterConfiguration != null && !string.IsNullOrEmpty(_configuration.FooterConfiguration.Text))
                        {
                            text.Span($"\r\n{_configuration.FooterConfiguration.Text}")
                                .FontFamily(_configuration.FooterConfiguration.Font.Name)
                                .FontColor(_configuration.FooterConfiguration.TextColor.ToHexString())
                                .Bold();
                        }

                    });
                });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent(_invoice.SellerAddress, _configuration.AddressConfiguration.SellerHeader, _configuration.AddressConfiguration.ShowLabels));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new AddressComponent(_invoice.CustomerAddress, _configuration.AddressConfiguration.CustomerHeader, _configuration.AddressConfiguration.ShowLabels));
                });

                if (CanRenderItemsTable())
                {
                    column.Item().Element(c => c.AlignCenter().Text("Purchased Items").Bold().FontColor(Colors.Blue.Medium).FontSize(16));
                    column.Item().Component(new ItemTableComponent(_configuration.ItemTableConfiguration, _invoice.Items, _invoice.InvoiceCurrency));
                }

                if (CanRenderPaymentsTable())
                {
                    column.Item().Element(c => c.AlignCenter().Text("Invoice Payments").Bold().FontColor(Colors.Blue.Medium).FontSize(16));
                    column.Item().Component(new PaymentTableComponent(_configuration.PaymentTableConfiguration, _invoice.Payments, _invoice.InvoiceCurrency));
                }

                column.Item().Component(new TotalComponent(_invoice.Items, _invoice.InvoiceCurrency));

                if (!string.IsNullOrWhiteSpace(_invoice.Note))
                    column.Item().PaddingTop(25).Component(new NoteComponent(_invoice.Note));
            });
        }

        private bool CanRenderItemsTable()
        {
            if (_invoice.Items is { Count: > 0 }) return true;

            if (_invoice.Items == null || _invoice.Items.Count == 0)
                return _configuration.ItemTableConfiguration.DisplayWithoutItems;

            return false;
        }

        private bool CanRenderPaymentsTable()
        {
            if (_invoice.Payments is { Count: > 0 }) return true;

            if (_invoice.Payments == null || _invoice.Payments.Count == 0)
                return _configuration.PaymentTableConfiguration.DisplayWithoutItems;

            return false;
        }
    }
}
