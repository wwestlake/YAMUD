using Microsoft.EntityFrameworkCore;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.Model.Utilities;
using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.API;

public class YamudDbContext : DbContext
{
    public YamudDbContext(DbContextOptions<YamudDbContext> options)
        : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Item> Items { get; set; }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Tool> Tools { get; set; }

    public DbSet<Money> Money { get; set; }
    public DbSet<Resource> Resources { get; set; }

    public DbSet<Weapon> Weapons { get; set; }

    public DbSet<CodeModule> CodeModules { get; set; }
    public DbSet<Character> Characters { get; set; }

    public DbSet<Achievement> Achievements { get; set; }

    public DbSet<Quest> Quests { get; set; }
    public DbSet<QuestSection> QuestSections { get; set; }
    public DbSet<QuestStep> QuestSteps { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
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

        modelBuilder.Entity<Annotation>()
            .HasOne(a => a.User) // Configure one-to-one relationship with ApplicationUser
            .WithMany() // Annotation can have one user, user can have many annotations
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Annotations) // Character can have many annotations
            .WithOne() // Annotation can belong to one character
            .HasForeignKey(a => a.AnnotatedEntityId); // Foreign key for Character

    }
}
