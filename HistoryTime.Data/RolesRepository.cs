using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HistoryTime.Domain;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Npgsql;

namespace HistoryTime.Data
{
    public class RolesRepository : ConnectionRepository, IRepository<Role>
    {
        public RolesRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from roles", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            var roles = new List<Role>();
            while (await reader.ReadAsync())
            {
                var role = new Role
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                roles.Add(role);
            }

            await Connection.CloseAsync();
            return roles;
        }

        public async Task<Role> Get(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"select * from roles where id={id}", Connection);
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var role = new Role
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                await Connection.CloseAsync();
                return role;
            }

            await Connection.CloseAsync();
            return null;
        }

        public async Task Update(Role role)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"update roles set name = '{role.Name}' where id = {role.Id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Create(Role role)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"insert into roles(name) values('{role.Name}')", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await Connection.OpenAsync();
            var command = new NpgsqlCommand($"delete from roles where id={id}", Connection);
            await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();
        }
    }
}