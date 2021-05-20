using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAnswers()
        {
            var answers = await _answersRepository.GetAll();
            return Ok(answers);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var answer = await _answersRepository.Get(id);
            if (answer == null)
                return NotFound();
            answer.UsersAnswers = await _answersRepository.GetUsersAnswers(answer.Id);
            answer.AnswerTheQuestions = await _answersRepository.GetAnswerTheQuestions(answer.Id);
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(Answer answer)
        {
            await _answersRepository.Create(new Answer
            {
                Text = answer.Text
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAnswer(int id)
        {
            await _answersRepository.Delete(id);
            return Ok();
        }
    }
}
