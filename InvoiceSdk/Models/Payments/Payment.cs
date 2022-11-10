namespace InvoiceSdk.Models.Payments
{
    public class Payment : IComparable<Payment>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }

        public int CompareTo(Payment other)
        {
            int idCmp = Id.CompareTo(other.Id);
            if (idCmp != 0) return idCmp;

            int amountCmp = Amount.CompareTo(other.Amount);
            if (amountCmp != 0) return amountCmp;

            int methodNameCmp = string.Compare(Method.Name, other.Method.Name, StringComparison.InvariantCultureIgnoreCase);
            int methodProviderUrlCmp = string.Compare(Method.ProviderUrl, other.Method.ProviderUrl, StringComparison.InvariantCultureIgnoreCase);

            if (methodNameCmp != 0) return methodNameCmp;
            if (methodProviderUrlCmp != 0) return methodProviderUrlCmp;

            int statusCmp = Status.CompareTo(other.Status);
            if (statusCmp != 0) return statusCmp;

            int paidAtCmp = PaidAt.CompareTo(other.PaidAt);
            if (paidAtCmp != 0) return paidAtCmp;

            return 0;
        }
    }
}
