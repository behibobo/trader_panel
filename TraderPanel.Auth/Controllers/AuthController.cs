using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TraderPanel.Auth.Models;
using TraderPanel.Auth.Services;
using TraderPanel.Core.Entities;
using TraderPanel.Core.Repositories;
using TraderPanel.Core.Repositories.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace TraderPanel.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private UserRepository _repository;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new UserRepository("users", _configuration);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _repository.GetByLoginAsync(model.LoginName);

            if (user == null || !BC.Verify(model.Password, user.Password))
            {
                return Unauthorized();
            }
            else
            {
                TokenHandler._configuration = _configuration;
                return Ok(TokenHandler.CreateAccessToken(user));
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User();
            user.Id = 1;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.UserType = model.UserType;
            user.Password = BC.HashPassword(model.Password);
            user.CreatedAt = DateTime.Now;
            user.ModifiedAt = DateTime.Now;
            await _repository.InsertAsync(user);
            return Ok();
        }
    }
}
