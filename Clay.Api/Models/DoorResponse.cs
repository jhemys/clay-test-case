using Clay.Application.DTOs;

namespace Clay.Api.Models
{
    public class DoorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAccessRestricted { get; set; }
        public IList<RoleResponse> AllowedRoles { get; set; }
    }
}
