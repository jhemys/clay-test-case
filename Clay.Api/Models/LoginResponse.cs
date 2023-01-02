using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
