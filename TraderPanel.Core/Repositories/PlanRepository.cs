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
    public class PlanRepository :  GenericRepository<Plan>
    {
        public PlanRepository(string tableName, IConfiguration configuration) : base(tableName, configuration)
        {

        }
    }
}
