using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersAnswersRepository _usersAnswersRepository;

        public UsersController(IUsersRepository usersRepository, IUsersAnswersRepository usersAnswersRepository)
        {
            _usersRepository = usersRepository;
            _usersAnswersRepository = usersAnswersRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _usersRepository.Get();
            return Ok(users);
        }

        [Route("{name}")]
        [HttpGet]
        public IActionResult Get(string name)
        {
            var user = _usersRepository.Get(name);
            if (user == null)
                return NotFound();
            user.UserAnswers = _usersAnswersRepository.Get(user.Id);
            return Ok(user);
        }
        
        [HttpPost]
        public IActionResult Register(string name)
        {
            _usersRepository.Create(new User
            {
                Name = name
            });
            return Ok(_usersRepository.Get(name));
        }

        [HttpDelete]
        public IActionResult RemoveUser(string name)
        {
            var user = _usersRepository.Get(name);
            _usersRepository.Delete(user.Id);
            return Ok();
        }
    }
}