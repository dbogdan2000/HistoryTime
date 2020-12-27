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
        public IActionResult GetQuizzes()
        {
            var quizzes = _quizzesRepository.Get();
            return Ok(quizzes);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var quiz = _quizzesRepository.Get(id);
            if (quiz == null)
                return NotFound();
            quiz.Questions = _quizzesRepository.GetQuestions(quiz.Id);
            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult AddQuiz(Quiz quiz)
        {
            _quizzesRepository.Create(new Quiz
            {
                Theme = quiz.Theme
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteQuiz(int id)
        {
            _quizzesRepository.Delete(id);
            return Ok(_quizzesRepository.Get());
        }
    }
}