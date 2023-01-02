using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class DoorHistoryResponse
    {
        public int DoorId { get; set; }
        public string DoorName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CurrentState { get; set; }
        public string? Message { get; set; }
        public DateTime LogDate { get; set; }
        public string? TagIdentification { get; set; }
        public bool IsRemoteAttempt { get; set; }
    }
}
