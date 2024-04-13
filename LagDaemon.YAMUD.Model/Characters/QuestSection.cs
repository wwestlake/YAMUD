using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Characters
{
    public class QuestSection
    {
        public QuestSection()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Icon { get; set; }

        public QuestGoal SectionGoal { get; set; }
        public ICollection<QuestStep> Step { get; set; }


    }
}
