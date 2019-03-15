using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(QuestTask), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<QuestTask>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTasks(int questId, int? taskId)
        {
            var result = await _taskRepository.ReadAll(t => t.Quest.Id == questId && (taskId == null || taskId == t.Id));

            if (taskId != null && result.Count == 0)
            {
                return NotFound();
            }

            return taskId == null ? Ok(result) : Ok(result.First());
        }
    }
}
