using Microsoft.EntityFrameworkCore;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.Items;

namespace LagDaemon.YAMUD.API;

public class YamudDbContext : DbContext
{

    public YamudDbContext(DbContextOptions<YamudDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PlayerState>()
           .HasKey(ps => ps.Id);

        // Configure foreign key relationship
        modelBuilder.Entity<PlayerState>()
            .HasOne(ps => ps.UserAccount)        // Navigation property
            .WithMany()                          // PlayerState can belong to only one UserAccount
            .HasForeignKey(ps => ps.UserAccountId);  // Foreign key property

    }
}
