using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IList<QuestLine>> GetQuests(int? id = null)
        {
            return await _questRepository.ReadAll(ql => id == null || ql.Id == id);
        }
    }
}
