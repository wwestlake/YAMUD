using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Characters
{
    public class CharacterQuestProgress
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public List<QuestProgress> QuestsProgress { get; set; }
    }

    public class QuestProgress
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        public List<SectionProgress> SectionsProgress { get; set; }
    }

    public class SectionProgress
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public List<Guid> CompletedStepIds { get; set; }
    }

}
