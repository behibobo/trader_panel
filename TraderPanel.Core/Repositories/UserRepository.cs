using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TraderPanel.Core.Entities;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Core.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private IConfiguration _configuration;
        public UserRepository(string tableName, IConfiguration configuration) : base(tableName, configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<User>($"SELECT * FROM public.users WHERE UserName=@login OR Email=@login", new { login = login });
                return result;
            }
        }


        private NpgsqlConnection connection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        private IDbConnection CreateConnection()
        {
            var conn = connection();
            conn.Open();
            return conn;
        }
    }
}
