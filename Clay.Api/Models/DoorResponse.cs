using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class DoorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAccessRestricted { get; set; }
        public bool IsActive { get; set; }
        public IList<RoleResponse> AllowedRoles { get; set; }
    }
}
