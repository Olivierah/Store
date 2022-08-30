using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Repository.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            //Tabela
            builder.ToTable("USER");


            //Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("USER_NAME")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("HASH")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Cpf)
               .IsRequired()
               .HasColumnName("CPF")
               .HasColumnType("VARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasColumnName("STREET")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.ZipCode)
                .IsRequired()
                .HasColumnName("ZIP_CODE")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.StreetNumber)
                .IsRequired()
                .HasColumnName("STREET_NUMBER")
                .HasColumnType("NUMERIC");

            builder.Property(x => x.Complement)
                .IsRequired(false)
                .HasColumnName("COMPLEMENT")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnName("BIRTHDATE")
                .HasColumnType("DATE");

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasColumnName("GENDER");


            // Índice
            builder.HasIndex(x => x.Email, "IX_USER_EMAIL").IsUnique();
            builder.HasIndex(x => x.Cpf, "IX_USER_CPF").IsUnique();

            // Relacionamentos

            // 1 -> N
            builder.HasMany(x => x.CreditCard)
              .WithOne(x => x.User)
              .HasConstraintName("FK_USER_CARD")
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
