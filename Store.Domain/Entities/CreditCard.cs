namespace Store.Domain.Entities
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string NameInCreditCard { get; set; }
        public string ExpirationDate { get; set; }
        public User User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
