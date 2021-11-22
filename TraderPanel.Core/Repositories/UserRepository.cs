using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TraderPanel.Core.Entities;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(User entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            var sql = "Insert into public.users (username, email, password, usertype) VALUES (@UserName,@Email,@Password, @UserType)";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM public.users WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM users";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }


        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM users WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            entity.ModifiedAt = DateTime.Now;
            var sql = "UPDATE users SET UserName = @UserName, Email = @Email, Password = @Password";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<User> GetByUserName(string loginName)
        {
            var sql = "SELECT * FROM public.users WHERE username = @loginName OR email = @loginName LIMIT 1";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { loginName = loginName });
                return result;
            }
        }
    }
}
