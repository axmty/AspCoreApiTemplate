using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAEngine.Core.Models;
using QAEngine.Core.Services;

namespace QAEngine.Api.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return this.Ok(await _questionsService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return this.Ok(await _questionsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateAsync(QuestionCreate question)
        {
            return this.Ok(await _questionsService.CreateAsync(question));
        }
    }
}
