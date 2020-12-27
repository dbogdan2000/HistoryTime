using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/answers")]
    public class AnswersController: Controller
    {
        private readonly IAnswersRepository _answersRepository;

        public AnswersController(IAnswersRepository answersRepository)
        {
            _answersRepository = answersRepository;
        }

        [HttpGet]
        public IActionResult GetAnswers()
        {
            var answers = _answersRepository.Get();
            return Ok(answers);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var answer = _answersRepository.Get(id);
            if (answer == null)
                return NotFound();
            answer.UsersAnswers = _answersRepository.GetUsersAnswers(answer.Id);
            answer.AnswerTheQuestions = _answersRepository.GetAnswerTheQuestions(answer.Id);
            return Ok(answer);
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            _answersRepository.Create(new Answer
            {
                Text = answer.Text
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoveAnswer(int id)
        {
            _answersRepository.Delete(id);
            return Ok();
        }
    }
}
