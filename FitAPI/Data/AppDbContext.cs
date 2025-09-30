using Microsoft.EntityFrameworkCore;
using FitAPI.Models;
using FitAPI.Services;

namespace FitAPI.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kunde> Kunden { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var passwordService = new PasswordService();

        modelBuilder.Entity<Kunde>().HasData(
            new Kunde
            {
                Id = 1,
                Vorname = "Max",
                Name = "Mustermann",
                MemberSince = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
                SubscriptionValidUntil = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                Email = "max@example.com",
                Phone = "123456789",
                PasswordHash = "password" 
            }
        );

        modelBuilder.Entity<Kunde>(e =>
        {
            e.HasIndex(k => k.Email).IsUnique();
            e.Property(k => k.MemberSince).HasDefaultValueSql("getutcdate()");
        });

        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Name = "SuperAdmin",
                Email = "Admin@example.com",
                PasswordHash = "10000.k5lGr0JPTaQ=.+GVujDgQ1dNYx7ZIl9N5EqhHj/E=" 
            }
        );

        modelBuilder.Entity<Admin>()
            .HasIndex(a => a.Email)
            .IsUnique();
    }


}