using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class AnswersTheQuestionsRepository : ConnectionRepository, IAnswersTheQuestionsRepository
    {
        public AnswersTheQuestionsRepository(string connectionString) : base(connectionString)
        {
           
        }


        public async Task<IEnumerable<AnswerTheQuestion>> GetAll()
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from answers_the_questions", Connection);
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

        public async Task<AnswerTheQuestion> GetCorrectAnswerOnQuestion(int questionId, bool isCorrect)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"select * from answers_the_questions where question_id = {questionId} and is_correct = {isCorrect} ",
                    Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var answerTheQuestion = new AnswerTheQuestion
                {
                    QuestionId = reader.GetInt32(0),
                    AnswerId = reader.GetInt32(1),
                    IsCorrect = reader.GetBoolean(2)
                };
                await Connection.CloseAsync();
                return answerTheQuestion;
            }

            await Connection.CloseAsync();
            return null;
        }

        public async Task<AnswerTheQuestion> Get(int questionId, int answerId)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"select * from answers_the_questions where question_id = {questionId} and answer_id = {answerId}",
                    Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var answerTheQuestion = new AnswerTheQuestion
                {
                    QuestionId = reader.GetInt32(0),
                    AnswerId = reader.GetInt32(1),
                    IsCorrect = reader.GetBoolean(2)
                };
                await Connection.CloseAsync();
                return answerTheQuestion;
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

        public async Task Update(AnswerTheQuestion answerTheQuestion)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"update answers_the_questions set is_correct = {answerTheQuestion.IsCorrect} where question_id = {answerTheQuestion.QuestionId} and answer_id = {answerTheQuestion.AnswerId}",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Create(AnswerTheQuestion answerTheQuestion)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"insert into answers_the_questions(question_id, answer_id, is_correct) values({answerTheQuestion.QuestionId}, {answerTheQuestion.AnswerId}, {answerTheQuestion.IsCorrect})",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int questionId, int answerId)
        {
            await Connection.OpenAsync();
            var command =
                new NpgsqlCommand(
                    $"delete from answers_the_questions where question_id = {questionId} and answer_id = {answerId}",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}