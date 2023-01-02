﻿using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class EmployeeRequest
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? TagIdentification { get; set; }
    }
}
