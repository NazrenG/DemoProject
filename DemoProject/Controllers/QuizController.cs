using DemoProject.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.DataAccess.Concrete;
using TaskFlow.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/<QuizController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _quizService.Quizzes();
            var items = list.Select(l =>
            {
                return new QuizDto
                {
                    AgeRange = l.AgeRange,
                    Profession = l.Profession,
                    UsagePurpose = l.UsagePurpose,
                };
            });
            return Ok(items);
        }

     
        // POST api/<QuizController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuizDto value)
        {
            var item = new Quiz
            {
                AgeRange = value.AgeRange,
                Profession = value.Profession,
                UsagePurpose = value.UsagePurpose,
            };
            await _quizService.Add(item);
            return Ok(item);
        }

        
    }
}
