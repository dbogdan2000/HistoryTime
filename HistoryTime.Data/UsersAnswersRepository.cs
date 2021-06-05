using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class UsersAnswersRepository : ConnectionRepository, IUsersAnswersRepository
    {
        public UsersAnswersRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<UserAnswer>> GetAll()
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand("select * from users_answers", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var usersAnswers = new List<UserAnswer>();
            while (await reader.ReadAsync())
            {
                var userAnswer = new UserAnswer
                {
                    UserId = reader.GetInt32(0),
                    QuestionId = reader.GetInt32(1),
                    AnswerId = reader.GetInt32(2)
                };
                usersAnswers.Add(userAnswer);
            }
            await Connection.CloseAsync();
            return usersAnswers;
        }

        public async Task<UserAnswer> Get(int userId, int answerId, int questionId)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"select * from users_answers where user_id = {userId} and answer_id = {answerId} and question_id = {questionId}",
                    Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var userAnswer = new UserAnswer
                {
                    UserId = reader.GetInt32(0),
                    AnswerId = reader.GetInt32(1)
                };
                await Connection.CloseAsync();
                return userAnswer;
            }
            await Connection.CloseAsync();
            return null;
        }

        public async Task<User> GetUser(int userId)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from users where id = {userId}", Connection);
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
                    DateOfBirth = reader.GetDateTime(6)
                };
                await Connection.CloseAsync();
                return user;
            }
            await Connection.CloseAsync();
            return null;
        }

        public async Task<Answer> GetAnswer(int answerId)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from answers where id = {answerId}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var answer = new Answer
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1)
                };
                await Connection.CloseAsync();
                return answer;
            }

            await Connection.CloseAsync();
            return null;
        }

        public async Task<Question> GetQuestion(int questionId)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from questions where id = {questionId}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var question = new Question
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1),
                    QuizId = reader.GetInt32(2)
                };
                await Connection.CloseAsync();
                return question;
            }
            await Connection.CloseAsync();
            return null;
        }

        public async Task Create(UserAnswer userAnswer)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"insert into users_answers(user_id, question_id, answer_id) values({userAnswer.UserId}, {userAnswer.QuestionId}, {userAnswer.AnswerId})",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int answerId, int userId, int questionId)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"delete from users_answers where answer_id = {answerId} and user_id = {userId} and question_id = {questionId}",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}