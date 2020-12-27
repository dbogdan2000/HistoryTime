using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HistoryTime.Domain;
using System.Data.SqlClient;
using Npgsql;

namespace HistoryTime.Data
{
    public class RolesRepository : IRolesRepository
    {
        private readonly string _connectionString;

        public RolesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Role> Get()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from roles", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                var roles = new List<Role>();
                while (reader.Read())
                {
                    var role = new Role();
                    role.Id = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                    roles.Add(role);
                }
                return roles;
            }

        }

        public Role Get(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"select * from roles where id={id}", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var role = new Role();
                    role.Id = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                    return role;
                }

                return null;
            }
        }
        
        public void Create(Role role)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"insert into roles(name) values('{role.Name}')", connection);
                int number = command.ExecuteNonQuery();
            }
            
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"delete from roles where id={id}", connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}