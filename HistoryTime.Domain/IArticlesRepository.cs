namespace HistoryTime.Domain
{
    public interface IArticlesRepository
    {
        Article[] Get();
        
        Article Get(int id);
        
        Article Get(string header);

        void Create(Article article);

        void Delete(int id);
    }
}