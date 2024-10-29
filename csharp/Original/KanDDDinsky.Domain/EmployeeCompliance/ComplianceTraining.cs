namespace KanDDDinsky.EmployeeCompliance;

public class ComplianceTraining
{
    public string TrainingName { get; private set; }
    public bool IsMandatory { get; private set; }

    public ComplianceTraining(string trainingName, bool isMandatory)
    {
        TrainingName = trainingName;
        IsMandatory = isMandatory;
    }
}

public readonly struct EmployeeId
{
    private readonly Guid _value;
    public EmployeeId(Guid value) => _value = value;
    public override string ToString() => _value.ToString();
}

public readonly struct CertificationId
{
    private readonly Guid _value;
    public CertificationId(Guid value) => _value = value;
    public override string ToString() => _value.ToString();
}

public enum ComplianceStatus
{
    Compliant,
    NonCompliant,
    InProgress
}

