using InvoiceSdk.Models;
using InvoiceSdk.Models.Payments;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer.Components
{
    internal class PaymentTableComponent : IComponent
    {
        private readonly PaymentTableConfiguration _configuration;
        private readonly List<Payment> _invoicePayments;
        private readonly InvoiceCurrencySymbol _invoiceCurrencySymbol;

        public PaymentTableComponent(PaymentTableConfiguration configuration, List<Payment> invoicePayments, InvoiceCurrencySymbol invoiceCurrencySymbol)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _invoiceCurrencySymbol = invoiceCurrencySymbol ?? throw new ArgumentNullException(nameof(invoiceCurrencySymbol));

            _invoicePayments = invoicePayments;
        }


        public void Compose(IContainer container)
        {
            TextStyle headerStyle = TextStyle.Default.SemiBold();

            if (_invoicePayments == null || _invoicePayments.Count == 0)
            {
                container.ShowEntire().Background(Colors.Red.Lighten5).Padding(10).Column(column =>
                {
                    column.Spacing(5);
                    column.Item().Text(_configuration.NoPaymentsHeaderText).FontColor(Colors.Red.Medium).FontSize(14).SemiBold();
                    column.Item().Text(_configuration.NoPaymentsNotificationText);
                });

                return;
            }

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1F);
                    columns.RelativeColumn(1.2F);
                    columns.RelativeColumn(2F);
                    columns.RelativeColumn(2F);
                    columns.RelativeColumn(0.5F);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Processor").Style(headerStyle);
                    header.Cell().Text("Amount").Style(headerStyle);
                    header.Cell().Text("Paid At").Style(headerStyle);
                    header.Cell().Text("Processed At").Style(headerStyle);
                    header.Cell().AlignRight().Text("Status").Style(headerStyle);

                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                foreach (Payment payment in _invoicePayments)
                {
                    table.Cell().Element(CellStyle).Text(payment.Method.Name);
                    table.Cell().Element(CellStyle).Text($"{payment.Amount}{_invoiceCurrencySymbol.GetSymbol()}");
                    table.Cell().Element(CellStyle).Text(payment.PaidAt.ToString("g"));
                    table.Cell().Element(CellStyle).Text(payment.ProcessedAt.ToString("g"));
                    table.Cell().Element(CellStyle).AlignRight().Text($"{payment.Status:F}");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }
    }
}
