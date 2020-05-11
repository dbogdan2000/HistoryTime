using System.Collections.Generic;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/usersAnswers")]
    public class UsersAnswersController : Controller
    {
        private readonly IUsersAnswersRepository _usersAnswersRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IUsersRepository _usersRepository;

        public UsersAnswersController(IUsersAnswersRepository usersAnswersRepository, IAnswersRepository answersRepository, IUsersRepository usersRepository)
        {
            _usersAnswersRepository = usersAnswersRepository;
            _answersRepository = answersRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult GetUsersAnswers()
        {
            var usersAnswers = _usersAnswersRepository.Get();
            return Ok(usersAnswers);
        }

        [Route("{name}")]
        [HttpGet]
        public IActionResult GetAnswersOfUser(string name)
        {
            var user = _usersRepository.Get(name);
            if (user == null)
                return NotFound();
            var userAnswers = _usersAnswersRepository.Get(user.Id);
            var answers = new List<Answer>();
            foreach (var userAnswer in userAnswers)
            {
                answers.Add(_answersRepository.Get(userAnswer.AnswerId));
            }

            return Ok(answers);
        }

        [HttpPost]
        public IActionResult AddUserAnswer(int userId, int answerId)
        {
            _usersAnswersRepository.Create(new UserAnswer
            {
                UserId = userId,
                AnswerId = answerId
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUserAnswer(int answerId, int userId)
        {
            _usersAnswersRepository.Delete(answerId, userId);
            return Ok(_usersAnswersRepository.Get(userId));
        }

    }
}