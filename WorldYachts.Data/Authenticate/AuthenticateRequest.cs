using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } //TODO: Хранить хеш
    }
}
