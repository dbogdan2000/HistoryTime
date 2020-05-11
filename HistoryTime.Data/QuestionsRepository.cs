using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class QuestionsRepository: IQuestionsRepository
    {
        private readonly string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IEnumerable<Question> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from questions", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var questions = new List<Question>();
                while (reader.Read())
                {
                    var question = new Question();
                    question.Id = reader.GetInt32(0);
                    question.Text = reader.GetString(1);
                    question.QuizId = reader.GetInt32(2);
                    questions.Add(question);
                }

                return questions;
            }
        }

        public Question Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from questions where id = {id}", connection);
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

        public Question Get(string text)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from questions where text = '{text}'", connection);
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
        
        public Quiz GetQuiz(int quizId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from quizzes where id = {quizId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var quiz = new Quiz();
                    quiz.Id = reader.GetInt32(0);
                    quiz.Name = reader.GetString(1);
                    return quiz;
                }

                return null;
            }
        }

        public IEnumerable<Answer> GetAnswers(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where question_id = {id}", connection);
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

        public void Create(Question question)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into questions(text,quiz_id) values('{question.Text}',  {question.QuizId})", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from questions where id = {id}", connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}