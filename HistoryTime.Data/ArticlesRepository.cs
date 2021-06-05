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
            await Connection.OpenAsync();
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
            await Connection.CloseAsync();
            return articles;
        }

        public async Task<Article> Get(int id)
        {
            await Connection.OpenAsync();
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
                await Connection.CloseAsync();
                return article;
            }
            await Connection.CloseAsync();
            return null;
        }

        public async Task Update(Article article)
        {
            await Connection.OpenAsync();
            Connection.Open();
            var command =
                new NpgsqlCommand($"update articles set header='{article.Header}', text_of_article='{article.Text}', author='{article.Author}' where id={article.Id}",
                    Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Create(Article article)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand(
                $"insert into articles(header, text_of_article, author) values('{article.Header}', '{article.Text}', '{article.Author}')",
                Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"delete from articles where id = {id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}