using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Repository.Mappings
{
    public class AppMap : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            //Tabela
            builder.ToTable("APP");


            //Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.AppName)
                .IsRequired()
                .HasColumnName("APP_NAME")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);
            
           

        }
    }
}
