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
        
        [Route("{header}")]
        [HttpGet]
        public IActionResult Get(string header)
        {
            var article = _articlesRepository.Get(header);
            if (article == null)
                return NotFound();
            return Ok(article);
        }

        [HttpPost]
        public IActionResult AddArticle(string header, string text)
        {
            _articlesRepository.Create(new Article
            {
                Header = header,
                Text = text
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteArticle(string header)
        {
            var article = _articlesRepository.Get(header);
            _articlesRepository.Delete(article.Id);
            return Ok(_articlesRepository.Get());
        }

    }
}