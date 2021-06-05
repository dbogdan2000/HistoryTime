using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class QuizzesRepository : ConnectionRepository, IQuizzesRepository
    {
        public QuizzesRepository(string connectionString) : base(connectionString)
        {
            
        }

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand("select * from quizzes", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var quizzes = new List<Quiz>();
            while (await reader.ReadAsync())
            {
                var quiz = new Quiz
                {
                    Id = reader.GetInt32(0),
                    Theme = reader.GetString(1)
                };
                quizzes.Add(quiz);
            }

            await Connection.CloseAsync();
            return quizzes;
        }

        public async Task<Quiz> Get(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from quizzes where id={id}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var quiz = new Quiz
                {
                    Id = reader.GetInt32(0),
                    Theme = reader.GetString(1)
                };
                await Connection.CloseAsync();
                return quiz;
            }

            await Connection.CloseAsync();
            return null;
        }

        public async Task<IEnumerable<Question>> GetQuestions(int quizId)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from questions where quiz_id = {quizId}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var questions = new List<Question>();
            while (await reader.ReadAsync())
            {
                var question = new Question
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1),
                    QuizId = reader.GetInt32(2)
                };
                questions.Add(question);
            }

            await Connection.CloseAsync();
            return questions;
        }
        
        public async Task Update(Quiz quiz)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"update quizzes set theme = '{quiz.Theme}' where id = {quiz.Id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Create(Quiz quiz)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"insert into quizzes(theme) values('{quiz.Theme}')", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"delete from quizzes where id={id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}