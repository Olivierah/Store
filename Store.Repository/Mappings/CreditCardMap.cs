using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Repository.Mappings
{
    internal class CreditCardMap : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {

            //Tabela
            builder.ToTable("CREDIT_CARD");

            //Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.CreditCardNumber)
                .IsRequired()
                .HasColumnName("CREDIT_CARD_NUMBER")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.NameInCreditCard)
                .IsRequired()
                .HasColumnName("NAME_IN_CREDIT_CARD")
                .HasColumnType("VARCHAR")
                .HasMaxLength(30);

            builder.Property(x => x.ExpirationDate)
                .IsRequired()
                .HasColumnName("EXPIRATION_DATE")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            
        }
    }
}
