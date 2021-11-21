using System;
using System.Threading.Tasks;
using TraderPanel.Core.Entities;
namespace TraderPanel.Core.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserName(string loginName);
    }
}