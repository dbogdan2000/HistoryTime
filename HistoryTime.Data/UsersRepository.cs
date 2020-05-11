using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class UsersRepository: IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<User> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from users", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var users = new List<User>();
                while (reader.Read())
                {
                    var user = new User();
                    user.Id = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.RoleId = reader.GetInt32(2);
                    users.Add(user);
                }
                return users;
            }
        }

        public User Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users where id={id}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = new User();
                    user.Id = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.RoleId = reader.GetInt32(2);
                }
                return null;
            }
        }

        public User Get(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users where name = '{name}'", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    var user = new User();
                    user.Id = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.RoleId = reader.GetInt32(2);
                    return user;
                }

                return null;
            }
        }

        public IEnumerable<UserAnswer> GetUserAnswers(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users_answers where user_id = {userId}");
                NpgsqlDataReader reader = command.ExecuteReader();
                var userAnswers = new List<UserAnswer>();
                while (reader.Read())
                {
                    var userAnswer = new UserAnswer();
                    userAnswer.UserId = reader.GetInt32(0);
                    userAnswer.AnswerId = reader.GetInt32(1);
                    userAnswers.Add(userAnswer);
                }

                return userAnswers;
            }
        }

        public void Create(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into users(name, role_id) values('{user.Name}',(select id from roles where name = 'User'))", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from users where id = {id}", connection);
                int number = command.ExecuteNonQuery();
            }         
        }
    }
}