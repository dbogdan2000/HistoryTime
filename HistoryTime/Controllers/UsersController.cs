using System.Threading.Tasks;
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersRepository.GetAll();
            return Ok(users);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _usersRepository.Get(id);
            if (user == null)
                return NotFound();
            user.UserAnswers = await _usersRepository.GetUserAnswers(id);
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            await _usersRepository.Create(new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
            return Ok(await _usersRepository.Get(user.Id));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            await _usersRepository.Delete(id);
            return Ok();
        }
    }
}