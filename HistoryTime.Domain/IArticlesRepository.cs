using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IArticlesRepository
    {
        IEnumerable<Article> Get();
        
        Article Get(int id);

        void Create(Article article);

        void Delete(int id);
    }
}