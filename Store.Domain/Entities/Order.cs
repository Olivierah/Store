namespace Store.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<App> AppList { get; set; }
        public CreditCard? creditCard { get; set; }
        public User Buyer { get; set; }

        public Order()
        {
            AppList = new List<App>();
            Buyer = new User();
        }
    }
}
