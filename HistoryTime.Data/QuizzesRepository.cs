using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;

namespace HistoryTime.Data
{
    public class QuizzesRepository: IQuizzesRepository
    {
        private readonly ICollection<Quiz> _quizzes = new List<Quiz>();

        public Quiz[] Get()
        {
            return _quizzes.ToArray();
        }

        public Question[] GetQuestions(int id)
        {
            foreach (var quiz in _quizzes)
            {
                if (quiz.Id == id)
                {
                    return quiz.Questions.ToArray();
                }
            }
            return null;
        }

        public Quiz Get(int id)
        {
            foreach (var quiz in _quizzes)
            {
                if (quiz.Id == id)
                {
                    return quiz;
                }
            }

            return null;
        }

        public Quiz Get(string name)
        {
            foreach (var quiz in _quizzes)
            {
                if (quiz.Name == name)
                {
                    return quiz;
                }
            }

            return null;
        }

      

        public void Create(Quiz quiz)
        {
            _quizzes.Add(quiz);
        }

        public void Delete(int id)
        {
            foreach (var quiz in _quizzes)
            {
                if (quiz.Id == id)
                {
                    _quizzes.Remove(quiz);
                    return;
                }
            }
        }

    }
}