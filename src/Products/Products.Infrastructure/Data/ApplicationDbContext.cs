using Microsoft.EntityFrameworkCore;
using Products.Domain.Models.Entities;

namespace Products.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Name)
            .IsRequired()
            .HasColumnName("Nombre")
            .HasMaxLength(200);

        base.OnModelCreating(modelBuilder);
    }
}
