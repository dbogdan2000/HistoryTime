using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/answers")]
    public class AnswersController: Controller
    {
        private readonly IAnswersRepository _answersRepository;
        private readonly IQuestionsRepository _questionsRepository;

        public AnswersController(IAnswersRepository answersRepository, IQuestionsRepository questionsRepository)
        {
            _answersRepository = answersRepository;
            _questionsRepository = questionsRepository;
        }

        [HttpGet]
        public IActionResult GetAnswers()
        {
            var answers = _answersRepository.Get();
            return Ok(answers);
        }

        [Route("{text}")]
        [HttpGet]
        public IActionResult Get(string text)
        {
            var answer = _answersRepository.Get(text);
            if (answer == null)
                return NotFound();
            answer.UsersAnswers = _answersRepository.GetUsersAnswers(answer.Id);
            answer.Question = _answersRepository.GetQuestion(answer.QuestionId);
            return Ok(answer);
        }

        [HttpPost]
        public IActionResult AddAnswer(string text, bool correct, string questionText)
        {
            var question = _questionsRepository.Get(questionText);
            if (question == null)
                return NotFound();
            _answersRepository.Create(new Answer
            {
                Text = text,
                IsCorrect = correct,
                QuestionId = question.Id
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoveAnswer(string text)
        {
            var answer = _answersRepository.Get(text);
            _answersRepository.Delete(answer.Id);
            return Ok();
        }
    }
}
