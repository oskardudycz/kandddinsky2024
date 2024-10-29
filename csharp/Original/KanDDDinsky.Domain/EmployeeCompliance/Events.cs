namespace KanDDDinsky.EmployeeCompliance;

public class ComplianceMetEvent
{
    public EmployeeId EmployeeId { get; }
    public ComplianceMetEvent(EmployeeId employeeId) => EmployeeId = employeeId;
}

public class ComplianceBreachEvent
{
    public EmployeeId EmployeeId { get; }
    public ComplianceBreachEvent(EmployeeId employeeId) => EmployeeId = employeeId;
}

public class CompliancePendingEvent
{
    public EmployeeId EmployeeId { get; }
    public CompliancePendingEvent(EmployeeId employeeId) => EmployeeId = employeeId;
}

