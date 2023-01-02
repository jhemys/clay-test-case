namespace Clay.Api.Models
{
    public class LoginCreationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? TagIdentification { get; set; }
        public string PermissionType { get; set; }
    }
}
