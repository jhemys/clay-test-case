using Clay.Domain.DomainObjects;
using Clay.Domain.ValueObjects;

namespace Clay.Domain.Aggregates.DoorHistory
{
    public class DoorHistory : Entity, IAggregateRoot
    {
        public int EmployeeId { get; protected set; }
        public string EmployeeName { get; protected set; }
        public string CurrentState { get; set; }
        public string? Message { get; protected set; }
        public DateTime LogDate { get; protected set; }
        public string? TagIdentification { get; protected set; }
        public bool IsRemoteAttempt => string.IsNullOrEmpty(TagIdentification);

        private DoorHistory() : base() { }

        protected DoorHistory(int employeeId, string employeeName, string currentState, DateTime logDate, string? tagIdentification, string? message) : this()
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            CurrentState = currentState;
            LogDate = logDate;
            Message = message;
            TagIdentification = tagIdentification;
        }

        public static DoorHistory Create(int employeeId, string employeeName, string currentState, DateTime logDate, string? tagIdentification, string? message = null)
        {
            return new DoorHistory(employeeId, employeeName, currentState, logDate, tagIdentification, message);
        }
    }
}
