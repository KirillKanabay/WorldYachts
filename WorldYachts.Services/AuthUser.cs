using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services
{
    public class AuthUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }

        public void Authenticate(AuthenticateResponse authenticateResponse)
        {
            Username = authenticateResponse.Username;
            Email = authenticateResponse.Email;
            Role = authenticateResponse.Role;
            UserId = authenticateResponse.UserId;
            Token = authenticateResponse.Token;
        }
    }
}
