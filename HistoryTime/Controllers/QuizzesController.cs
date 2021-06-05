using System.Threading.Tasks;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/quizzes")]
    public class QuizzesController : Controller
    {
        private readonly IQuizzesRepository _quizzesRepository;

        public QuizzesController(IQuizzesRepository quizzesRepository)
        {
            _quizzesRepository = quizzesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizzes = await _quizzesRepository.GetAll();
            return Ok(quizzes);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var quiz = await _quizzesRepository.Get(id);
            if (quiz == null)
                return NotFound();
            quiz.Questions = await _quizzesRepository.GetQuestions(quiz.Id);
            return Ok(quiz);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuiz(Quiz quiz)
        {
            await _quizzesRepository.Update(quiz);
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuiz(Quiz quiz)
        {
            await _quizzesRepository.Create(new Quiz
            {
                Theme = quiz.Theme
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            await _quizzesRepository.Delete(id);
            return Ok(await _quizzesRepository.GetAll());
        }
    }
}