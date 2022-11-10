using InvoiceSdk.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer.Components
{
    internal class TotalComponent : IComponent
    {
        private readonly List<InvoiceItem> _items;
        private readonly InvoiceCurrencySymbol _currencySymbol;

        public TotalComponent(List<InvoiceItem> items, InvoiceCurrencySymbol currencySymbol)
        {
            _currencySymbol = currencySymbol ?? throw new ArgumentNullException(nameof(currencySymbol));
            
            _items = items;
        }

        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().PaddingRight(5).AlignRight().Text(text =>
                {
                    text.Span("Subtotal:").SemiBold();

                    if (_items == null || _items.Count == 0)
                        text.Span($" N/A");
                    else
                        text.Span($" {GetTotalWithoutVat()}{_currencySymbol.GetSymbol()}");
                });

                column.Item().PaddingRight(5).AlignRight().Text(text =>
                {
                    text.Span("VAT:").SemiBold();

                    if (_items == null || _items.Count == 0)
                        text.Span($" N/A");
                    else
                        text.Span($" {GetVat()}%");
                });

                column.Item().PaddingRight(5).AlignRight().Text(text =>
                {
                    text.Span("Grand total:").SemiBold();

                    if (_items == null || _items.Count == 0)
                        text.Span($" N/A");
                    else
                        text.Span($" {GetTotalWithVat()}{_currencySymbol.GetSymbol()}");
                });
            });
        }

        private decimal GetTotalWithVat()
        {
            return _items.Sum(x => x.PriceWithVat * x.Quantity);
        }

        private decimal GetTotalWithoutVat()
        {
            return _items.Sum(x => x.UnitPriceWithoutVat * x.Quantity);
        }

        private decimal GetVat()
        {
            return _items.Max(x => x.VatPercentage);
        }
    }
}
