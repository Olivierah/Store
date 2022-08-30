using Store.Domain.Enum;

namespace Store.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Cpf { get; set; }        
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public int StreetNumber { get; set; }
        public string? Complement { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public List<CreditCard>? CreditCard { get; set; }        
    }
}
