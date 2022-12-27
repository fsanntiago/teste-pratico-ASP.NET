using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteEstagio.Models;

namespace TesteEstagio.Data.Mappings;

public class SupplierMap : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        // Table
        builder.ToTable("Supplier", x => x.HasCheckConstraint("CK_Specialty",
            $"[Specialty]='{Specialties.Comercio}' OR [Specialty]='{Specialties.Industria}' OR [Specialty]='{Specialties.Servico}'"));

        // Primary key
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.CNPJ)
            .IsRequired()
            .HasColumnName("CNPJ")
            .HasColumnType("CHAR")
            .HasMaxLength(14);

        builder.Property(x => x.Specialty)
            .IsRequired()
            .HasColumnName("Specialty")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .HasConversion(new EnumToStringConverter<Specialties>());

        // Indices
        builder.HasIndex(x => x.CNPJ, "UQ_Supplier_CNPJ")
            .IsUnique();
    }
}