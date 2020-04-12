using System;
using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;

namespace HistoryTime.Data
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly ICollection<Answer> _answers = new List<Answer>();
        public Answer[] Get()
        {
            return _answers.ToArray();
        }

        public Answer Get(int id)
        {
            foreach (var answer in _answers)
            {
                if (answer.Id == id)
                {
                    return answer;
                }
            }

            return null;
        }

        public Answer Get(string text)
        {
            foreach (var answer in _answers)
            {
                if (answer.Text == text)
                {
                    return answer;
                }
            }

            return null;
        }

        public Answer Get(bool isCorrect)
        {
            foreach (var answer in _answers)
            {
                if (answer.IsCorrect == isCorrect)
                {
                    return answer;
                }
            }

            return null;
        }

        public Question GetQuestion(int id)
        {
            foreach (var answer in _answers)
            {
                if (answer.Id == id)
                {
                    return answer.Question;
                }
            }

            return null;
        }

        public void Create(Answer answer)
        {
            _answers.Add(answer);
        }

        public void Delete(int id)
        {
            foreach (var answer in _answers)
            {
                if (answer.Id == id)
                {
                    _answers.Remove(answer);
                    return;
                }
            }
        }
    }
}