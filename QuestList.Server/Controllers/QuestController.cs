using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestList.Shared.Interfaces;
using QuestList.Shared.Models;

namespace QuestList.Server.Controllers
{
    [ApiController]
    [Route("quests")]
    public class QuestController : Controller
    {
        private readonly IRepository<QuestLine> _questRepository;

        public QuestController(IRepository<QuestLine> questRepository)
        {
            _questRepository = questRepository;
        }

        [HttpGet("{id?}")]
        [ProducesResponseType(typeof(QuestLine), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<QuestLine>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuests(int? id = null)
        {
            var result = await _questRepository.ReadAll(ql => id == null || ql.Id == id);

            if (id != null && result.Count == 0)
            {
                return NotFound();
            }

            return id == null ? Ok(result) : Ok(result.First());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuest(int id, QuestLine quest)
        {
            if (quest.Id != id)
            {
                return BadRequest();
            }

            return Ok(await _questRepository.Update(quest));
        }
    }
}
