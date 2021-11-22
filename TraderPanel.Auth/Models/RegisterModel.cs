using System;
using TraderPanel.Core.Entities;

namespace TraderPanel.Auth.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
    }
}
