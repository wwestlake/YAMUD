using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Characters
{
    public class QuestStep
    {
        public QuestStep()
        {
            Id = Guid.NewGuid();
        }
        public int Order { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public QuestGoal StepGoal {get; set; }
    }
}
