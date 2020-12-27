using System.Linq;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;

        public ArticlesController(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet]
        public IActionResult GetArticles()
        {
            var articles = _articlesRepository.Get();
            return Ok(articles);
        }
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var article = _articlesRepository.Get(id);
            if (article == null)
                return NotFound();
            return Ok(article);
        }

        [HttpPost]
        public IActionResult AddArticle(Article article)
        {
            Article articleToCreate = new Article
            {
                Header = article.Header,
                Text = article.Text,
                Author = article.Author
            };
            _articlesRepository.Create(articleToCreate);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            _articlesRepository.Delete(id);
            return Ok(_articlesRepository.Get());
        }

    }
}