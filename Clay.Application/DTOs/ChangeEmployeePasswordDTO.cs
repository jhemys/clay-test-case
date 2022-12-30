namespace Clay.Application.DTOs
{
    public class ChangeEmployeePasswordDTO
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
