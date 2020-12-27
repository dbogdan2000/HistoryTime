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
                var command = new NpgsqlCommand($"select * from answers", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var answers = new List<Answer>();
                while (reader.Read())
                {
                    var answer = new Answer();
                    answer.Id = reader.GetInt32(0);
                    answer.Text = reader.GetString(1);
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
                    return answer;
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
                    userAnswer.QuestionId = reader.GetInt32(1);
                    userAnswer.AnswerId = reader.GetInt32(2);
                    usersAnswers.Add(userAnswer);
                }

                return usersAnswers;
            }
        }

        public ICollection<AnswerTheQuestion> GetAnswerTheQuestions(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers_the_questions where answer_id = {id}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var answersTheQuestions = new List<AnswerTheQuestion>();
                while (reader.Read())
                {
                    var answerTheQuestion = new AnswerTheQuestion();
                    answerTheQuestion.QuestionId = reader.GetInt32(0);
                    answerTheQuestion.AnswerId = reader.GetInt32(1);
                    answerTheQuestion.IsCorrect = reader.GetBoolean(2);
                    answersTheQuestions.Add(answerTheQuestion);
                }

                return answersTheQuestions;
            }
        }

        public void Create(Answer answer)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into answers(text_of_answer) values('{answer.Text}')", connection);
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