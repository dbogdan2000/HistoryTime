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

        [Route("{name}")]
        [HttpGet]
        public IActionResult Get(string name)
        {
            var quiz = _quizzesRepository.Get(name);
            if (quiz == null)
                return NotFound();
            quiz.Questions = _quizzesRepository.GetQuestions(quiz.Id);
            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult AddQuiz(string name)
        {
            _quizzesRepository.Create(new Quiz
            {
                Name = name
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteQuiz(string name)
        {
            var quiz = _quizzesRepository.Get(name);
            _quizzesRepository.Delete(quiz.Id);
            return Ok(_quizzesRepository.Get());
        }
    }
}