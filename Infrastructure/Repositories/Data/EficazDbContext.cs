using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Data;

public partial class EficazDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Marca> Marca { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    public EficazDbContext() { }

    public EficazDbContext(DbContextOptions<EficazDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
        .HasCharSet("utf8mb4");

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Address>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<Address>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        // Definindo o relacionamento entre User e Address
        modelBuilder.Entity<User>()
            .HasMany(u => u.Enderecos)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Enderecos)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Marca>()
           .HasKey(m => m.Id);
        modelBuilder.Entity<Marca>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>()
           .HasKey(c => c.Id);
        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
           .HasKey(p => p.Id);
        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Marca)
            .WithMany(m => m.Produtos)
            .HasForeignKey(p => p.MarcaId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
