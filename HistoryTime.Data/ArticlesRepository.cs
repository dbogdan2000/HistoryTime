using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;
using Npgsql;

namespace HistoryTime.Data
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly string _connectionString;

        public ArticlesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IEnumerable<Article> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from articles", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var articles = new List<Article>();
                while (reader.Read())
                {
                    var article = new Article();
                    article.Id = reader.GetInt32(0);
                    article.Header = reader.GetString(1);
                    article.Text = reader.GetString(2);
                    article.Author = reader.GetString(3);
                    articles.Add(article);
                }

                return articles;
            }
        }

        public Article Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from articles where id = {id}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var article = new Article();
                    article.Id = reader.GetInt32(0);
                    article.Header = reader.GetString(1);
                    article.Text = reader.GetString(2);
                    article.Author = reader.GetString(3);
                    return article;
                }

                return null;
            }
        }

        public void Create(Article article)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into articles(header, text_of_article, author) values('{article.Header}', '{article.Text}', '{article.Author}')", connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from articles where id = {id}", connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}