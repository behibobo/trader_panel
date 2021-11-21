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
    public class PlanRepository : IPlanRepository
    {
        private readonly IConfiguration configuration;
        public PlanRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Plan entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            var sql = "Insert into plans (Name,TraderRate,PanelRate,CustomerRate,CreatedAt, ModifiedAt) VALUES (@Name,@TradeRate,@PanelRate,@CustomerRate,@CreatedAt, ModifiedAt)";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM plans WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Plan>> GetAllAsync()
        {
            var sql = "SELECT * FROM plans";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Plan>(sql);
                return result.ToList();
            }
        }

        public Task<List<Plan>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<Plan> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM plans WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Plan>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Plan entity)
        {
            entity.ModifiedAt = DateTime.Now;
            var sql = "UPDATE plans SET Name = @Name, TraderRate = @TraderRate, PanelRate = @PanelRate, CustomerRate = @CustomerRate, ModifiedAt = @ModifiedAt  WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
