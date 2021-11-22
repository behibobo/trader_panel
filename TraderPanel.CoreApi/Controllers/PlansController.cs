using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public PlansController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }        

        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            return Ok(await _repository.Plans.GetAllAsync());
        }
    }
}
