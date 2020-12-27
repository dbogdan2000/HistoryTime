using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IQuestionsRepository
    {
        IEnumerable<Question> Get();
        
        Question Get(int id);
        Quiz GetQuiz(int quizId);

        IEnumerable<AnswerTheQuestion> GetAnswersTheQuestion(int id);

        IEnumerable<UserAnswer> GetUsersAnswers(int id);

        void Create(Question question);

        void Delete(int id);
    }
}