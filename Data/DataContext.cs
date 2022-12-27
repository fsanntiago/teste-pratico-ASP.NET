using Microsoft.EntityFrameworkCore;
using TesteEstagio.Data.Mappings;
using TesteEstagio.Models;

namespace TesteEstagio.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SupplierMap());
    }
}