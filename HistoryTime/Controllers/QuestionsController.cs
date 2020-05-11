using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IQuizzesRepository _quizzesRepository;

        public QuestionsController(IQuestionsRepository questionsRepository, IQuizzesRepository quizzesRepository)
        {
            _questionsRepository = questionsRepository;
            _quizzesRepository = quizzesRepository;
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            var questions = _questionsRepository.Get();
            return Ok(questions);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var question = _questionsRepository.Get(id);
            if (question == null)
                return NotFound();
            question.Answers = _questionsRepository.GetAnswers(question.Id);
            question.Quiz = _questionsRepository.GetQuiz(question.QuizId);
            return Ok(question);
        }

        [HttpPost]
        public IActionResult AddQuestion(string quizName, string text)
        {
            var quiz = _quizzesRepository.Get(quizName);
            if (quiz == null)
                return NotFound();
            _questionsRepository.Create(new Question
            {
                Text = text,
                QuizId = quiz.Id
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            _questionsRepository.Delete(id);
            return Ok();
        }

    }
}