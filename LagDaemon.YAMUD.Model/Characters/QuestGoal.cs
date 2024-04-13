using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Characters
{
    public class QuestGoal
    {
        public QuestGoal()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

    }
}
