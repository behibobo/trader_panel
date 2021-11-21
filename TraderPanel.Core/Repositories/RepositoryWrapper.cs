using System;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Core.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        public RepositoryWrapper(
            IPlanRepository planRepository,
            ICustomerRepository customerRepository
        )
        {
            Plans = planRepository;
            Customers = customerRepository;
        }
        public IPlanRepository Plans { get; }
        public ICustomerRepository Customers { get; }
    }
}
