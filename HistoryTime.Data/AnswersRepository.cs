using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class AnswersRepository : ConnectionRepository, IAnswersRepository
    {
        public AnswersRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            var command = new NpgsqlCommand("select * from answers", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var answers = new List<Answer>();
            while (await reader.ReadAsync())
            {
                var answer = new Answer
                {
                    Id = reader.GetInt32(0),
                    Text = reader.GetString(1)
                };
                answers.Add(answer);
            }

            return answers;
        }

        public async Task<Answer> Get(int id)
        {
            var command = new NpgsqlCommand($"select * from answers where id = {id}", Connection);

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var answer = new Answer
                {
                    Id = reader.GetInt32(0), 
                    Text = reader.GetString(1)
                };
                return answer;
            }

            return null;
        }

        public async Task<ICollection<UserAnswer>> GetUsersAnswers(int id)
        {
            var command = new NpgsqlCommand($"select * from users_answers where answer_id = {id}", Connection);
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

            return usersAnswers;
        }

        public async Task<ICollection<AnswerTheQuestion>> GetAnswerTheQuestions(int id)
        {
            var command = new NpgsqlCommand($"select * from answers_the_questions where answer_id = {id}", Connection);
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

            return answersTheQuestions;
        }

        public async Task Create(Answer answer)
        {
            var command = new NpgsqlCommand($"insert into answers(text_of_answer) values('{answer.Text}')", Connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(int id)
        {
            var command = new NpgsqlCommand($"delete from answers where id = {id}", Connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}