using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class UsersRepository : ConnectionRepository, IUsersRepository
    {
        public UsersRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var command = new NpgsqlCommand($"select * from users", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var users = new List<User>();
            while (await reader.ReadAsync())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    RoleId = reader.GetInt32(2),
                    Surname = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Email = reader.GetString(5),
                    DateOfBirth = reader.GetDateTime(6).Date
                };
                users.Add(user);
            }

            return users;
        }

        public async Task<User> Get(int id)
        {
            var command = new NpgsqlCommand($"select * from users where id={id}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    RoleId = reader.GetInt32(2),
                    Surname = reader.GetString(3),
                    Patronymic = reader.GetString(4),
                    Email = reader.GetString(5),
                    DateOfBirth = reader.GetDateTime(6).Date
                };
                return user;
            }

            return null;
        }

        public async Task<IEnumerable<UserAnswer>> GetUserAnswers(int userId)
        {
            var command = new NpgsqlCommand($"select * from users_answers where user_id = {userId}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var userAnswers = new List<UserAnswer>();
            while (await reader.ReadAsync())
            {
                var userAnswer = new UserAnswer
                {
                    UserId = reader.GetInt32(0),
                    QuestionId = reader.GetInt32(1),
                    AnswerId = reader.GetInt32(2)
                };
                userAnswers.Add(userAnswer);
            }

            return userAnswers;
        }

        public async Task<Role> GetRole(int roleId)
        {
            var command = new NpgsqlCommand($"select * from roles where id = {roleId}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var role = new Role()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetName(1)
                };
                return role;
            }

            return null;
        }

        public async Task Create(User user)
        {
            var command = new NpgsqlCommand(
                $"insert into users(first_name, role_id, last_name, patronymic, email, date_of_birth) values('{user.Name}', 2, '{user.Surname}', '{user.Patronymic}', '{user.Email}','{user.DateOfBirth.Date.ToString("d")}')",
                Connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(int id)
        {
            var command = new NpgsqlCommand($"delete from users where id = {id}", Connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}