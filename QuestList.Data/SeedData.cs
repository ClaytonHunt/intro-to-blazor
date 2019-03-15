using System.Linq;
using QuestList.Shared.Models;

namespace QuestList.Data
{
    public static class SeedData
    {
        public static void Initialize(QuestLineContext db)
        {
            var quests = new QuestLine[]
            {
                new QuestLine
                {
                    Id = 1,
                    Name = "Miscellaneous",
                    IsPermanent = true
                },
                new QuestLine
                {
                    Id = 2,
                    Name = "Create Todo List",
                    IsPermanent = true
                }
            };

            var tasks = new QuestTask[]
            {
                new QuestTask
                {
                    Id = 1,
                    Name = "Make the todo list look like Skyrim",
                    CreatedBy = "Clayton Hunt",
                    IsCompleted = false,
                    RequiresVerification = false
                },
                new QuestTask
                {
                    Id = 2,
                    Name = "Add Sub Tasks",
                    CreatedBy = "Clayton Hunt",
                    IsCompleted = false,
                    RequiresVerification = false
                },
                new QuestTask
                {
                    Id = 3,
                    Name = "Add Task Rewards",
                    CreatedBy = "Clayton Hunt",
                    IsCompleted = false,
                    RequiresVerification = false
                },
                new QuestTask
                {
                    Id = 4,
                    Name = "Add Task Actions",
                    CreatedBy = "Clayton Hunt",
                    IsCompleted = false,
                    RequiresVerification = false
                }
            };

            quests.Single(q => q.Id == 1).Tasks = tasks.Where(t => new[] { 1 }.Any(x => x == t.Id)).ToList();
            quests.Single(q => q.Id == 2).Tasks = tasks.Where(t => new[] { 2, 3, 4 }.Any(x => x == t.Id)).ToList();

            db.Quests.AddRange(quests);
            db.Tasks.AddRange(tasks);
            db.SaveChanges();
        }
    }
}
