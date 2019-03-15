using System.Collections.Generic;

namespace QuestList.Shared.Models
{
    public class QuestTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool IsCompleted { get; set; }
        public bool RequiresVerification { get; set; }
        public QuestLine Quest { get; set; }
        public ICollection<Reward> Rewards { get; set; }
    }
}