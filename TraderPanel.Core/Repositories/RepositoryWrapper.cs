using System;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Core.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        public RepositoryWrapper(
            IPlanRepository planRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository
        )
        {
            Plans = planRepository;
            Customers = customerRepository;
            Users = userRepository;
        }
        public IPlanRepository Plans { get; }
        public ICustomerRepository Customers { get; }
        public IUserRepository Users { get; }
    }
}
