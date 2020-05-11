using System;
using System.Collections.Generic;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class UsersAnswersRepository : IUsersAnswersRepository
    {
        private readonly string _connectionString;

        public UsersAnswersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IEnumerable<UserAnswer> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from users_answers", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var usersAnswers = new List<UserAnswer>();
                while (reader.Read())
                {
                    var userAnswer = new UserAnswer();
                    userAnswer.UserId = reader.GetInt32(0);
                    userAnswer.AnswerId = reader.GetInt32(1);
                    usersAnswers.Add(userAnswer);
                }

                return usersAnswers;
            }
        }

        public IEnumerable<UserAnswer> Get(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users_answers where user_id = {userId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var usersAnswers = new List<UserAnswer>();
                while (reader.Read())
                {
                    var userAnswer = new UserAnswer();
                    userAnswer.UserId = reader.GetInt32(0);
                    userAnswer.AnswerId = reader.GetInt32(1);
                    usersAnswers.Add(userAnswer);
                }

                return usersAnswers;
            }
        }

        public UserAnswer Get(int userId, int answerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users_answers where user_id = {userId} and answer_id = {answerId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var userAnswer = new UserAnswer();
                    userAnswer.UserId = reader.GetInt32(0);
                    userAnswer.AnswerId = reader.GetInt32(1);
                    return userAnswer;
                }

                return null;
            }
        }

        public User GetUser(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users where id = {userId}", connection);
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

        public Answer GetAnswer(int answerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where id = {answerId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    var answer = new Answer();
                    answer.Id = reader.GetInt32(0);
                    answer.Text = reader.GetString(1);
                    answer.IsCorrect = reader.GetBoolean(2);
                    answer.QuestionId = reader.GetInt32(3);
                    return answer;
                }

                return null;
            }
        }

        public void Create(UserAnswer userAnswer)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into users_answers(user_id, answer_id) values({userAnswer.UserId}, {userAnswer.AnswerId})", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int answerId, int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from users_answers where answer_id = {answerId} and user_id = {userId}", connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}