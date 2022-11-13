namespace InvoiceSdk.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceWithoutVat { get; set; }
        public decimal PriceWithVat => (UnitPriceWithoutVat + (UnitPriceWithoutVat * VatPercentage / 100));
        public decimal VatPercentage { get; set; }
    }
}
