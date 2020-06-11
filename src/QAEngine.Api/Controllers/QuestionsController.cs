using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QAEngine.Domain.Resources;
using QAEngine.Domain.Services;

namespace QAEngine.Api.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ApiController
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet]
        public async Task<ActionResult> ListAsync()
        {
            return this.Ok(await _questionsService.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return this.Ok(await _questionsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<QuestionResponse>> CreateAsync([FromBody]QuestionCreateRequest question)
        {
            var createdQuestionID = await _questionsService.CreateAsync(question);
            return this.CreatedAtAction(nameof(GetAsync), new { id = createdQuestionID });
        }
    }
}
