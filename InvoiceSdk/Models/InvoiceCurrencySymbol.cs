namespace InvoiceSdk.Models
{
    public class InvoiceCurrencySymbol
    {
        public InvoiceCurrency Currency { get; }

        public InvoiceCurrencySymbol(InvoiceCurrency currency)
        {
            Currency = currency;
        }

        public string GetSymbol() =>
            Currency switch
            {
                InvoiceCurrency.Dollar => "$",
                InvoiceCurrency.Euro => "€",
                _ => throw new ArgumentOutOfRangeException(nameof(Currency))
            };
    }
}
