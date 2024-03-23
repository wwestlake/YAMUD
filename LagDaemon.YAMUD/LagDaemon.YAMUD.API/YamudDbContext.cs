using Microsoft.EntityFrameworkCore;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.API
{
    public class YamudDbContext : DbContext
    {
        private readonly string connectionString = "Server=localhost;Port=5432;Database=yamud;User Id=postgres;Password=htrv5gpl;";

        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
