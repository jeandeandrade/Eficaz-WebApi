using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Data;

public partial class EficazDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Address { get; set; }

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
