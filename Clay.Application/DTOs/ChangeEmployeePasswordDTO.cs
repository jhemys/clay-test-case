using System.Diagnostics.CodeAnalysis;

namespace Clay.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ChangePasswordDTO
    {
        public int Id { get; set; }
        public int CurrentLoginId { get; set; }
        public string CurrentLoginPermissionType { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
