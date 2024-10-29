namespace KanDDDinsky.EmployeeCompliance;

public class Certification
{
    public CertificationId Id { get; private set; }
    public string Name { get; private set; }
    public int ValidityPeriodInMonths { get; private set; }

    public Certification(CertificationId id, string name, int validityPeriodInMonths)
    {
        Id = id;
        Name = name;
        ValidityPeriodInMonths = validityPeriodInMonths;
    }
}

