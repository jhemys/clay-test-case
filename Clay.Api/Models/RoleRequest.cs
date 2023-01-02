using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class RoleRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
