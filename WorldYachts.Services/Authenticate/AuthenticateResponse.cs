using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Services.Authenticate
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
