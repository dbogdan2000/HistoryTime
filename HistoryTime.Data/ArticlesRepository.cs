using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class ArticlesRepository : ConnectionRepository, IRepository<Article>
    {
        public ArticlesRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var command = new NpgsqlCommand("select * from articles", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var articles = new List<Article>();
            while (await reader.ReadAsync())
            {
                var article = new Article
                {
                    Id = reader.GetInt32(0),
                    Header = reader.GetString(1),
                    Text = reader.GetString(2),
                    Author = reader.GetString(3)
                };
                articles.Add(article);
            }

            return articles;
        }

        public async Task<Article> Get(int id)
        {
            var command = new NpgsqlCommand($"select * from articles where id = {id}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var article = new Article
                {
                    Id = reader.GetInt32(0),
                    Header = reader.GetString(1),
                    Text = reader.GetString(2),
                    Author = reader.GetString(3)
                };
                return article;
            }

            return null;
        }

        public async Task Create(Article article)
        {
            var command = new NpgsqlCommand(
                $"insert into articles(header, text_of_article, author) values('{article.Header}', '{article.Text}', '{article.Author}')",
                Connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(int id)
        {
            var command = new NpgsqlCommand($"delete from articles where id = {id}", Connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}