using System;
using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly string _connectionString;

        public AnswersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Answer> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from answers", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var answers = new List<Answer>();
                while (reader.Read())
                {
                    var answer = new Answer();
                    answer.Id = reader.GetInt32(0);
                    answer.Text = reader.GetString(1);
                    answer.IsCorrect = reader.GetBoolean(2);
                    answer.QuestionId = reader.GetInt32(3);
                    answers.Add(answer);
                }

                return answers;
            }
        }

        public Answer Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where id = {id}", connection);
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

        public Answer Get(string text)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where text = '{text}'", connection);
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

        public Answer Get(bool isCorrect)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where is_correct = {isCorrect}", connection);
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

        public Question GetQuestion(int questionId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from questions where id = {questionId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var question = new Question();
                    question.Id = reader.GetInt32(0);
                    question.Text = reader.GetString(1);
                    question.QuizId = reader.GetInt32(2);
                    return question;
                }

                return null;
            }
        }

        public ICollection<UserAnswer> GetUsersAnswers(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users_answers where answer_id = {id}", connection);
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

        public void Create(Answer answer)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into answers(text, is_correct, question_id) values('{answer.Text}', {answer.IsCorrect}, {answer.QuestionId})", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from answers where id = {id}", connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}