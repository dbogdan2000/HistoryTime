using System.Threading.Tasks;
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
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _questionsRepository.GetAll();
            return Ok(questions);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var question = await _questionsRepository.Get(id);
            if (question == null)
                return NotFound();
            question.AnswerTheQuestions = await _questionsRepository.GetAnswersTheQuestion(id);
            question.UserAnswers = await _questionsRepository.GetUsersAnswers(id);
            question.Quiz = await _questionsRepository.GetQuiz(question.QuizId);
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            var quiz = await _questionsRepository.GetQuiz(question.QuizId);
            if (quiz == null)
                return NotFound();
            await _questionsRepository.Create(new Question
            {
                Text = question.Text,
                QuizId = quiz.Id
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _questionsRepository.Delete(id);
            return Ok();
        }

    }
}