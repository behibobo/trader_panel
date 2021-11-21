
using System;
namespace TraderPanel.Core.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IPlanRepository Plans { get; }
        ICustomerRepository Customers { get; }
    }
}
