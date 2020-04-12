using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;

namespace HistoryTime.Data
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ICollection<Article> _articles = new List<Article>();
        
        public Article[] Get()
        {
            return _articles.ToArray();
        }

        public Article Get(int id)
        {
            foreach (var article in _articles)
            {
                if (article.Id == id)
                {
                    return article;
                }
            }
            return null;
        }

        public Article Get(string header)
        {
            foreach (var article in _articles)
            {
                if (article.Header == header)
                {
                    return article;
                }
            }
            return null;
        }
        
        public void Create(Article article)
        {
            _articles.Add(article);
        }

        public void Delete(int id)
        {
            foreach (var article in _articles)
            {
                if (article.Id == id)
                {
                    _articles.Remove(article);
                    return;
                }
            }
        }
    }
}