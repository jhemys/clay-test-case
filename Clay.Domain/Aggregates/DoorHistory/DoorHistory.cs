using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain.Aggregates.DoorHistory
{
    public class DoorHistory : Entity
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public State CurrentState { get; set; }
        public string? Message { get; set; }
        public DateTime LogDate { get; set; }

        private DoorHistory() : base() { }

        protected DoorHistory(int employeeId, string employeeName, State currentState, DateTime logDate, string? message) : this()
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            CurrentState = currentState;
            LogDate = logDate;
            Message = message;
        }

        public static DoorHistory Create(int employeeId, string employeeName, State currentState, DateTime logDate, string? message = null)
        {
            return new DoorHistory(employeeId, employeeName, currentState, logDate, message);
        }
    }
}
