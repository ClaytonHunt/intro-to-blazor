using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestList.Shared.Interfaces;
using QuestList.Shared.Models;

namespace QuestList.Server.Controllers
{
    [ApiController]
    [Route("quests/{questId}/tasks")]
    public class TaskController : Controller
    {
        private readonly IRepository<QuestTask> _taskRepository;

        public TaskController(IRepository<QuestTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("{taskId?}")]
        public async Task<IList<QuestTask>> GetTasks(int questId, int? taskId)
        {
            return await _taskRepository.ReadAll(t => t.Quest.Id == questId && (taskId == null || taskId == t.Id));
        }
    }
}
