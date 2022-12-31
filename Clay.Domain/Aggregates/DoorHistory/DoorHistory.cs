using Clay.Domain.DomainObjects;

namespace Clay.Domain.Aggregates.DoorHistory
{
    public class DoorHistory : Entity, IAggregateRoot
    {
        public int DoorId { get; protected set; }
        public string DoorName { get; protected set; }
        public int EmployeeId { get; protected set; }
        public string EmployeeName { get; protected set; }
        public string CurrentState { get; protected set; }
        public string? Message { get; protected set; }
        public DateTime LogDate { get; protected set; }
        public string? TagIdentification { get; protected set; }
        public bool IsRemoteAttempt => string.IsNullOrEmpty(TagIdentification);

        private DoorHistory() : base() { }

        protected DoorHistory(int doorId, string doorName, int employeeId, string employeeName, string currentState, DateTime logDate, string? tagIdentification, string? message) : this()
        {
            DoorId = doorId;
            DoorName = doorName;
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            CurrentState = currentState;
            LogDate = logDate;
            Message = message;
            TagIdentification = tagIdentification;
        }

        public static DoorHistory Create(int doorId, string doorName, int employeeId, string employeeName, string currentState, DateTime logDate, string? tagIdentification, string? message = null)
        {
            return new DoorHistory(doorId, doorName, employeeId, employeeName, currentState, logDate, tagIdentification, message);
        }
    }
}
