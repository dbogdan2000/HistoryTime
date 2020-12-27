using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _usersRepository.Get();
            return Ok(users);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var user = _usersRepository.Get(id);
            if (user == null)
                return NotFound();
            user.UserAnswers = _usersRepository.GetUserAnswers(id);
            return Ok(user);
        }
        
        [HttpPost]
        public IActionResult Register(User user)
        {
            _usersRepository.Create(new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
            return Ok(_usersRepository.Get(user.Id));
        }

        [HttpDelete]
        public IActionResult RemoveUser(int id)
        {
            _usersRepository.Delete(id);
            return Ok();
        }
    }
}