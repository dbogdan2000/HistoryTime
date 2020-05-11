using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class QuizzesRepository: IQuizzesRepository
    {
        private readonly string _connectionString;

        public QuizzesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Quiz> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from quizzes", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var quizzes = new List<Quiz>();
                while (reader.Read())
                {
                    var quiz = new Quiz();
                    quiz.Id = reader.GetInt32(0);
                    quiz.Name = reader.GetString(1);
                    quizzes.Add(quiz);
                }
                return quizzes;
            }   
        }

        public IEnumerable<Question> GetQuestions(int quizId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from questions where quiz_id = {quizId}", connection);
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

        public Quiz Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from quizzes where id={id}", connection);
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

        public Quiz Get(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from quizzes where name='{name}'", connection);
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

      

        public void Create(Quiz quiz)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into quizzes(name) values('{quiz.Name}')", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from quizzes where id={id}", connection);
                int number = command.ExecuteNonQuery();
            }
        }

    }
}