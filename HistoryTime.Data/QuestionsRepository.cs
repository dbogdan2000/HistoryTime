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
                var command = new NpgsqlCommand($"select * from questions", connection);
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
                    quiz.Theme = reader.GetString(1);
                    return quiz;
                }

                return null;
            }
        }

        public IEnumerable<AnswerTheQuestion> GetAnswersTheQuestion(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers_the_questions where question_id = {id}", connection);
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

        public IEnumerable<UserAnswer> GetUsersAnswers(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from users_answers where question_id = {id}", connection);
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

        public void Create(Question question)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into questions(text_of_question, quiz_id) values('{question.Text}',  {question.QuizId})", connection);
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