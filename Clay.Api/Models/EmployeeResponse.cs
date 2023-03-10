using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string TagIdentification { get; set; }
        public bool IsActive { get; set; }
    }
}
