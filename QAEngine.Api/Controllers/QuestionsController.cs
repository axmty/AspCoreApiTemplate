using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QAEngine.Api.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly Data.QAEngineContext _context;

        public QuestionsController(Data.QAEngineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Question>>> GetAsync()
        {
            return (await _context.Questions.ToListAsync()).Select(q => new Models.Question
            {
                Content = q.Content,
                CreateDate = q.CreateDate
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Question>> GetAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return new Models.Question
            {
                Content = question.Content,
                CreateDate = question.CreateDate
            };
        }

        [HttpPost]
        public async Task<ActionResult<Models.Question>> CreateAsync(Models.QuestionCreate question)
        {
            var data = new Data.Question
            {
                Content = question.Content,
                CreateDate = DateTimeOffset.Now
            };

            _context.Questions.Add(data);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = data.Id }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
