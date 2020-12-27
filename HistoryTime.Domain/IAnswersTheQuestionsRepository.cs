using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IAnswersTheQuestionsRepository
    {
        IEnumerable<AnswerTheQuestion> Get();

        AnswerTheQuestion GetCorrectAnswerOnQuestion(int questionId, bool isCorrect);

        AnswerTheQuestion Get(int questionId, int answerId);

        Question GetQuestion(int questionId);

        Answer GetAnswer(int answerId);

        void Create(AnswerTheQuestion answerTheQuestion);

        void Delete(int questionId, int answerId);

    }
}