﻿namespace Clay.Application.DTOs
{
    public class DoorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAccessRestricted { get; set; }
        public IList<RoleDTO> AllowedRoles { get; set; }
    }
}