using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
