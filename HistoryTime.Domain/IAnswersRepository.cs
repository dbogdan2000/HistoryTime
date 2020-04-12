namespace HistoryTime.Domain
{
    public interface IAnswersRepository
    {
        Answer[] Get();
        
        Answer Get(int id);
        
        Answer Get(string text);
        
        Answer Get(bool isCorrect);

        Question GetQuestion(int id);

        void Create(Answer answer);

        void Delete(int id);
    }
}