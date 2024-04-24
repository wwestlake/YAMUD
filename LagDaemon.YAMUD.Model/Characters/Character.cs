using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.Characters;
public class Character
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int ExperiencePoints { get; set; }
    public int HealthPoints { get; set; }
    public int MaxHealthPoints { get; set; }
    public int ManaPoints { get; set; }
    public int MaxManaPoints { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }
    public int Luck { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public ICollection<Annotation> Annotations { get; set; } = new List<Annotation>();
    public ICollection<Item> Items { get; set; }
    public ICollection<Achievement> Achievements { get; set;}
    public ICollection<Guid> CompletedQuests { get; set; }
    public ICollection<Quest> ActiveQuests { get; set; }
    public ICollection<CharacterQuestProgress> QuestProgress { get; set; } = new List<CharacterQuestProgress>();



}
