namespace Clay.Application.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TagIdentification { get; set; }
        public bool IsActive { get; set; }
    }
}
