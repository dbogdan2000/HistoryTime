namespace HistoryTime.Domain
{
    public interface IQuizzesRepository
    {
        Quiz[] Get();

        Quiz Get(int id);

        Quiz Get(string name);

        Question[] GetQuestions(int id);

        void Create(Quiz quiz);

        void Delete(int id);
    }
}