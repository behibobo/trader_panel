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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration configuration;
        public CustomerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Customer entity)
        {
            entity.CreatedAt = DateTime.Now;
            var sql = "Insert into customers (FirstName,LastName,UserName,Email,CreatedAt) VALUES (@FirstName,@LastName,@UserName,@Email,@CreatedAt)";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM customers WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            var sql = "SELECT * FROM customers";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Customer>(sql);
                return result.ToList();
            }
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Customer entity)
        {
            entity.ModifiedAt = DateTime.Now;
            var sql = "UPDATE Products SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Email = @Email, ModifiedAt = @ModifiedAt  WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
