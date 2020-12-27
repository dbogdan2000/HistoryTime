using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
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
            question.AnswerTheQuestions = _questionsRepository.GetAnswersTheQuestion(id);
            question.UserAnswers = _questionsRepository.GetUsersAnswers(id);
            question.Quiz = _questionsRepository.GetQuiz(question.QuizId);
            return Ok(question);
        }

        [HttpPost]
        public IActionResult AddQuestion(Question question)
        {
            var quiz = _questionsRepository.GetQuiz(question.QuizId);
            if (quiz == null)
                return NotFound();
            _questionsRepository.Create(new Question
            {
                Text = question.Text,
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