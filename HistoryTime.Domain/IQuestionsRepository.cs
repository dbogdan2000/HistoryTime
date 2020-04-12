namespace HistoryTime.Domain
{
    public interface IQuestionsRepository
    {
        Question[] Get();
        
        Question Get(int id);
        
        Question Get(string text);

        Quiz GetQuizzes(int id);

        Answer[] GetAnswers(int id);

        void Create(Question question);

        void Delete(int id);
    }
}