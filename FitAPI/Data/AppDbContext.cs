using Microsoft.EntityFrameworkCore;
using FitAPI.Models;

namespace FitAPI.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kunde> Kunden { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Kunde>().HasData(
            new Kunde
            {
                Id = 1,
                Vorname = "Max",
                Name = "Mustermann",
                MemberSince = new DateTime(2025, 1, 1),
                SubscriptionValidUntil = new DateTime(2026, 1, 1),
                Email = "max@example.com",
                PasswordHash = "PLACEHOLDER_HASH",
                Phone = "123456789"

            }
        );
        modelBuilder.Entity<Kunde>(entity =>
        {
            entity.HasIndex(k => k.Email).IsUnique();
            entity.Property(k => k.MemberSince).HasDefaultValueSql("getutcdate()");
        });



        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Name = "SuperAdmin",
                Email = "Admin@example.com",
                PasswordHash = "PLACEHOLDER_HASH"
            }
        );
        modelBuilder.Entity<Admin>()
            .HasIndex(a => a.Email)
            .IsUnique();
    }

}