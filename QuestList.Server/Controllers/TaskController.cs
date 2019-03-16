﻿using System.Collections.Generic;
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
            var result = await GetQuestTasks(questId, taskId);

            if (taskId != null && result.Count == 0)
            {
                return NotFound();
            }

            return taskId == null ? Ok(result) : Ok(result.First());
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuest(int questId, int taskId, QuestTask task)
        {
            if (!IsMatchingTask(taskId, task) || !await IsQuestTask(questId, taskId))
            {
                return BadRequest();
            }

            return Ok(await _taskRepository.Update(task));
        }

        private async Task<bool> IsQuestTask(int questId, int taskId)
        {
            return (await GetQuestTasks(questId, taskId)).Any();
        }

        private static bool IsMatchingTask(int taskId, QuestTask task)
        {
            return task.Id == taskId;
        }

        private async Task<IList<QuestTask>> GetQuestTasks(int questId, int? taskId)
        {
            return await _taskRepository.ReadAll(t => t.Quest.Id == questId && (taskId == null || taskId == t.Id));
        }
    }
}
