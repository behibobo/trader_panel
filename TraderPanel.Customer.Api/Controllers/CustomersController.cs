using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public CustomersController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _repository.Customers.GetAllAsync());
        }

        [HttpGet("{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(name);
        }
    }
}
