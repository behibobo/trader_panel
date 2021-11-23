using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TraderPanel.Core.Entities;
using TraderPanel.Core.Models;
using TraderPanel.Core.Repositories;
using TraderPanel.Core.Repositories.Interfaces;

namespace TraderPanel.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly PlanRepository _repository;
        private readonly IConfiguration _configuration;
        public PlansController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new PlanRepository("plans", _configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var res = new Response();
            res.Result = await _repository.GetAllAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan([FromRoute] int id)
        {
            var res = new Response();
            var plan = await _repository.GetAsync(id);
            if (plan == null)
            {
                res.Status = 404;
                res.Success = false;
                return UnprocessableEntity(res);
            }
            res.Result = plan;
            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] Plan model)
        {
            var res = new Response();
            await _repository.InsertAsync(model);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan([FromRoute] int id, [FromBody] Plan model)
        {
            var res = new Response();
            var plan = await _repository.GetAsync(id);
            if (plan == null)
            {
                res.Status = 404;
                res.Success = false;
                return UnprocessableEntity(res);
            }
            model.Id = id;
            await _repository.UpdateAsync(model);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan([FromRoute] int id)
        {
            var res = new Response();
            await _repository.DeleteRowAsync(id);
            return Ok(res);
        }
    }
}
