using System;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Auth.Services
{
    public class AuthenticatioService
    {
        private readonly IRepositoryWrapper _repository;

        public AuthenticatioService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


    }
}
