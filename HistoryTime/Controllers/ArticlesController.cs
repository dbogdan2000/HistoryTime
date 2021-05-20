using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/articles")]
    public class ArticlesController : Controller
    {
        private readonly IRepository<Article> _articlesRepository;

        public ArticlesController(IRepository<Article> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articlesRepository.GetAll();
            return Ok(articles);
        }
        
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articlesRepository.Get(id);
            if (article == null)
                return NotFound();
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article)
        {
            Article articleToCreate = new Article
            {
                Header = article.Header,
                Text = article.Text,
                Author = article.Author
            };
            await _articlesRepository.Create(articleToCreate);
            return Ok(articleToCreate);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articlesRepository.Delete(id);
            return Ok(_articlesRepository.GetAll());
        }

    }
}