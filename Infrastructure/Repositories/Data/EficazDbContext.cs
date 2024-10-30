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
            .UseCollation("utf8mb4_0900_ai_ci")
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
