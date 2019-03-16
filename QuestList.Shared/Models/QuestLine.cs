using System.Collections.Generic;

namespace QuestList.Shared.Models
{
    public class QuestLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsBeingWorked { get; set; }
        public ICollection<QuestTask> Tasks { get; set; } = new List<QuestTask>();
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}