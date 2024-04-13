using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Characters
{
    public class Quest
    {
        public Quest()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public QuestGoal QuestGoal { get; set; }

        public ICollection<QuestSection> Sections { get; set; }
    }
}
