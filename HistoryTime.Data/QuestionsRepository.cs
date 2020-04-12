using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;

namespace HistoryTime.Data
{
    public class QuestionsRepository: IQuestionsRepository
    {
        private readonly ICollection<Question> _questions = new List<Question>();
        
        public Question[] Get()
        {
            return _questions.ToArray();
        }

        public Question Get(int id)
        {
            foreach (var question in _questions)
            {
                if (question.Id == id)
                {
                    return question;
                }
            }

            return null;
        }

        public Question Get(string text)
        {
            foreach (var question in _questions)
            {
                if (question.Text == text)
                {
                    return question;
                }
            }

            return null;
        }
        
        public Quiz GetQuizzes(int id)
        {
            foreach (var question in _questions)
            {
                if (question.Id == id)
                {
                    return question.Quiz;
                }
            }

            return null;
        }

        public Answer[] GetAnswers(int id)
        {
            foreach (var question in _questions)
            {
                if (question.Id == id)
                {
                    return question.Answers.ToArray();
                }
            }

            return null;
        }

        public void Create(Question question)
        {
            _questions.Add(question);
        }

        public void Delete(int id)
        {
            foreach (var question in _questions)
            {
                if (question.Id == id)
                {
                    _questions.Remove(question);
                    return;
                }
            }
        }
    }
}