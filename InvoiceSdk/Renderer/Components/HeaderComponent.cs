using InvoiceSdk.Helpers;
using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer.Components
{
    internal class HeaderComponent : IComponent
    {
        private readonly Invoice _invoice;
        private readonly HeaderConfiguration _headerConfiguration;

        public HeaderComponent(Invoice invoice, HeaderConfiguration headerConfiguration)
        {
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            _headerConfiguration = headerConfiguration ?? throw new ArgumentNullException(nameof(headerConfiguration));
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Invoice #{_invoice.Number}")
                        .FontSize(20).SemiBold().FontColor(_headerConfiguration.TextColor.ToHexString());

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"{_invoice.IssuedAt:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Due date: ").SemiBold();
                        text.Span($"{_invoice.DueAt:d}");
                    });

                    if (_invoice.UpdatedAt.HasValue)
                    {
                        column.Item().Text(text =>
                        {
                            text.Span("Updated at: ").SemiBold();
                            text.Span($"{_invoice.UpdatedAt:d}");
                        });
                    }
                });

                if (_headerConfiguration.LogoConfiguration != null && File.Exists(_headerConfiguration.LogoConfiguration.LogoSourceFile))
                    row.ConstantItem(100).Height(_headerConfiguration.LogoConfiguration.LogoHeightCm)
                        .Image(_headerConfiguration.LogoConfiguration.LogoSourceFile, ImageScaling.Resize);
            });
        }
    }
}
