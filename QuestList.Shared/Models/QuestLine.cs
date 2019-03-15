using System.Collections.Generic;

namespace QuestList.Shared.Models
{
    public class QuestLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPermanent { get; set; }
        public ICollection<QuestTask> Tasks { get; set; }
        public ICollection<Reward> Rewards { get; set; }
    }
}