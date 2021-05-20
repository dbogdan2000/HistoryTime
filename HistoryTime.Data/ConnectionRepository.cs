using System;
using System.Threading.Tasks;
using Npgsql;

namespace HistoryTime.Data
{
    public class ConnectionRepository : IDisposable, IAsyncDisposable
    {
        protected readonly NpgsqlConnection Connection;

        protected ConnectionRepository(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Connection.DisposeAsync();
        }
    }
}