using System.Diagnostics.CodeAnalysis;

namespace Clay.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class LoginDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string TagIdentification { get; set; }
        public string PermissionType { get; set; }
        public bool IsActive { get; set; }
    }
}
