using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost]
        //public async Task<ActionResult<Core.Models.QuestionRead>> CreateAsync(Core.Models.QuestionCreate question)
        //{
        //    var data = new Core.Data.Question
        //    {
        //        Content = question.Content,
        //        CreateDate = DateTimeOffset.Now
        //    };

        //    _context.Questions.Add(data);

        //    await _context.SaveChangesAsync();

        //    var response = new Core.Models.QuestionRead
        //    {
        //        Content = data.Content,
        //        CreateDate = data.CreateDate,
        //        ID = data.ID
        //    };

        //    return this.CreatedAtAction("Get", new { id = data.ID }, response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var question = await _context.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Questions.Remove(question);
        //    await _context.SaveChangesAsync();

        //    return this.NoContent();
        //}
    }
}
