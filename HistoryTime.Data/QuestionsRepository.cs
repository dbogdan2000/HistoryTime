using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class QuestionsRepository : ConnectionRepository, IQuestionsRepository
    {
        public QuestionsRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand("select * from questions", Connection);
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

        public async Task<Question> Get(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from questions where id = {id}", Connection);
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

        public async Task<Quiz> GetQuiz(int quizId)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from quizzes where id = {quizId}", Connection);
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

        public async Task<IEnumerable<AnswerTheQuestion>> GetAnswersTheQuestion(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from answers_the_questions where question_id = {id}",
                Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var answersTheQuestions = new List<AnswerTheQuestion>();
            while (await reader.ReadAsync())
            {
                var answerTheQuestion = new AnswerTheQuestion
                {
                    QuestionId = reader.GetInt32(0),
                    AnswerId = reader.GetInt32(1),
                    IsCorrect = reader.GetBoolean(2)
                };
                answersTheQuestions.Add(answerTheQuestion);
            }

            await Connection.CloseAsync();
            return answersTheQuestions;
        }

        public async Task<IEnumerable<UserAnswer>> GetUsersAnswers(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from users_answers where question_id = {id}", Connection);
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
        
        public async Task Update(Question question)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"update questions set text_of_question = '{question.Text}', quiz_id = {question.QuizId} where id = {question.Id}",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Create(Question question)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"insert into questions(text_of_question, quiz_id) values('{question.Text}', {question.QuizId})",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"delete from questions where id = {id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}