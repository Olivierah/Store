namespace Store.Domain.Entities
{
    public class App
    {
        public Guid Id { get; set; }
        public string AppName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
