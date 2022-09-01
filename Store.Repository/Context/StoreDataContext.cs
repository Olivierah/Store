using Microsoft.EntityFrameworkCore;
using Store.Repository.Mappings;
using Store.Domain.Entities;

namespace Store.Repository.Context
{
    public class StoreDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        
        //Definindo a conexão com o banco
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer("Server=127.0.0.1, 1433; Database=StoreDB; User Id=SA; Password=Senha12345");

        //Mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AppMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new CreditCardMap());
        }
    }
}
