using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain
{
    public class Log : Entity
    {
        public Log() { }
        public Log(int employeeId, string employeeName, State currentState, DateTime logDate)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            CurrentState = currentState;
            LogDate = logDate;
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public State CurrentState { get; set; }
        public DateTime LogDate { get; set; }
    }
}