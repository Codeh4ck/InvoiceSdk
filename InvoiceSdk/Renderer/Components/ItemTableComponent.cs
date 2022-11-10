using InvoiceSdk.Models;
using InvoiceSdk.Renderer.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer.Components
{
    internal class ItemTableComponent : IComponent
    {
        private readonly ItemTableConfiguration _configuration;
        private readonly List<InvoiceItem> _invoiceItems;
        private readonly InvoiceCurrencySymbol _invoiceCurrencySymbol;

        public ItemTableComponent(ItemTableConfiguration configuration, List<InvoiceItem> invoiceItems, InvoiceCurrencySymbol invoiceCurrencySymbol)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _invoiceCurrencySymbol = invoiceCurrencySymbol ?? throw new ArgumentNullException(nameof(invoiceCurrencySymbol));

            _invoiceItems = invoiceItems;
        }

        public void Compose(IContainer container)
        {
            TextStyle headerStyle = TextStyle.Default.SemiBold();

            if (_invoiceItems == null || _invoiceItems.Count == 0)
            {
                container.ShowEntire().Background(Colors.Red.Lighten5).Padding(10).Column(column =>
                {
                    column.Spacing(5);
                    column.Item().Text(_configuration.NoItemsNotificationHeader).FontColor(Colors.Red.Medium).FontSize(14).SemiBold();
                    column.Item().Text(_configuration.NoItemsNotificationText);
                });

                return;
            }

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Product").Style(headerStyle);
                    header.Cell().AlignRight().Text("Unit Price").Style(headerStyle);
                    header.Cell().AlignRight().Text("Units").Style(headerStyle);
                    header.Cell().AlignRight().Text("VAT").Style(headerStyle);
                    header.Cell().AlignRight().Text("Total").Style(headerStyle);

                    header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                foreach (InvoiceItem item in _invoiceItems)
                {
                    table.Cell().Element(CellStyle).Text(_invoiceItems.IndexOf(item) + 1);

                    if (!_configuration.DisplayItemDescriptions || string.IsNullOrEmpty(item.Description))
                        table.Cell().Element(CellStyle).Text(item.Name);
                    else
                    {
                        if (_configuration.DisplayItemDescriptions)
                        {
                            table.Cell().Element(CellStyle).Text(t =>
                            {
                                t.Span($"{item.Name}\r\n");
                                t.Span($" ({item.Description})").FontSize(_configuration.TableFont.Size - 0.3f);
                            });
                        }
                    }

                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPriceWithoutVat}{_invoiceCurrencySymbol.GetSymbol()}");
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.VatPercentage}%");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.PriceWithVat * item.Quantity}{_invoiceCurrencySymbol.GetSymbol()}");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }
    }
}
