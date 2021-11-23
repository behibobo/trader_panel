using Microsoft.Extensions.DependencyInjection;
using TraderPanel.Core.Repositories;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Core.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            //services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
