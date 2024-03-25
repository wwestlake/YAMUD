using Microsoft.EntityFrameworkCore;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Map;

namespace LagDaemon.YAMUD.API
{
    public class YamudDbContext : DbContext
    {

        public YamudDbContext(DbContextOptions<YamudDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.PlayerState)
                .WithOne(ps => ps.UserAccount)
                .HasForeignKey<PlayerState>(ps => ps.Id);
        }
    }
}
