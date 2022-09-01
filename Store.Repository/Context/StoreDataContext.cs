using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities;
using Store.Repository.Mappings;

namespace Store.Repository.Context
{    
    public class StoreDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }


        
       // protected readonly IConfiguration Configuration;
       //
       // public StoreDataContext(IConfiguration configuration)
       // {
       //     Configuration = configuration;
       // }
       //
       // //Definindo a conexão com o banco
       // protected override void OnConfiguring(DbContextOptionsBuilder options)
       // {
       //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
       // }
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
