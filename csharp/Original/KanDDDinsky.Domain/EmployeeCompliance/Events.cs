namespace KanDDDinsky.EmployeeCompliance;

public abstract class ComplianceEvent
{
    public class ComplianceMetEvent: ComplianceEvent
    {
        public EmployeeId EmployeeId { get; }
        public ComplianceMetEvent(EmployeeId employeeId) => EmployeeId = employeeId;
    }

    public class ComplianceBreachEvent: ComplianceEvent
    {
        public EmployeeId EmployeeId { get; }
        public ComplianceBreachEvent(EmployeeId employeeId) => EmployeeId = employeeId;
    }

    public class CompliancePendingEvent: ComplianceEvent
    {
        public EmployeeId EmployeeId { get; }
        public CompliancePendingEvent(EmployeeId employeeId) => EmployeeId = employeeId;
    }
}

