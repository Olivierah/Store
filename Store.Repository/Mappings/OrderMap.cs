using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Repository.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Tabela
            builder.ToTable("ORDER");


            //Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades

            // Relacionamentos

            builder.HasOne(x => x.creditCard)
              .WithMany(x => x.Orders)
              .HasConstraintName("FK_CREDITCARD_ORDER")
              .OnDelete(DeleteBehavior.NoAction);

             //N -> N   
              builder.HasMany(x => x.AppList)
                  .WithMany(x => x.Orders)
                  .UsingEntity<Dictionary<string, object>>(
                  "ORDER_APP",
                  app => app
                      .HasOne<App>()
                      .WithMany()
                      .HasForeignKey("App_Id")
                      .HasConstraintName("FK_OrderApp_AppId")
                      .OnDelete(DeleteBehavior.NoAction),
                  order => order
                      .HasOne<Order>()
                      .WithMany()
                      .HasForeignKey("OrderId")
                      .HasConstraintName("FK_OrderApp_OrderId")
                      .OnDelete(DeleteBehavior.NoAction));
        }
    }
}
