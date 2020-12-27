using System;
using System.Collections.Generic;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class AnswersTheQuestionsRepository : IAnswersTheQuestionsRepository
    {
        private readonly string _connectionString;

        public AnswersTheQuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IEnumerable<AnswerTheQuestion> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers_the_questions", connection);
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

        public AnswerTheQuestion GetCorrectAnswerOnQuestion(int questionId, bool isCorrect)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers_the_questions where question_id = {questionId} and is_correct = {isCorrect} ", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var answerTheQuestion = new AnswerTheQuestion();
                    answerTheQuestion.QuestionId = reader.GetInt32(0);
                    answerTheQuestion.AnswerId = reader.GetInt32(1);
                    answerTheQuestion.IsCorrect = reader.GetBoolean(2);
                    return answerTheQuestion;
                }

                return null;
            }
        }

        public AnswerTheQuestion Get(int questionId, int answerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers_the_questions where question_id = {questionId} and answer_id = {answerId}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var answerTheQuestion = new AnswerTheQuestion();
                    answerTheQuestion.QuestionId = reader.GetInt32(0);
                    answerTheQuestion.AnswerId = reader.GetInt32(1);
                    answerTheQuestion.IsCorrect = reader.GetBoolean(2);
                    return answerTheQuestion;
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

        public Answer GetAnswer(int answerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from answers where id = {answerId}", connection);
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

        public void Create(AnswerTheQuestion answerTheQuestion)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into answers_the_questions(question_id, answer_id, is_correct) values({answerTheQuestion.QuestionId}, {answerTheQuestion.AnswerId}, {answerTheQuestion.IsCorrect})", connection);
                var number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int questionId, int answerId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from answers_the_questions where question_id = {questionId} and answer_id = {answerId}", connection);
                var number = command.ExecuteNonQuery();
            }
        }
    }
}