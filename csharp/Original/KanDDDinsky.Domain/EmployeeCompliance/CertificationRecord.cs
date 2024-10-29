namespace KanDDDinsky.EmployeeCompliance;

public class CertificationRecord
{
    public Certification Certification { get; private set; }
    public DateTime DateIssued { get; private set; }
    public DateTime? RenewalDate { get; private set; }
    public bool IsExpired => RenewalDate.HasValue && RenewalDate.Value < DateTime.UtcNow;

    public CertificationRecord(Certification certification, DateTime dateIssued)
    {
        Certification = certification;
        DateIssued = dateIssued;
        RenewalDate = null;
    }

    public void Renew(DateTime renewalDate)
    {
        if (renewalDate <= DateIssued)
        {
            throw new InvalidOperationException("Renewal date cannot be before the issue date.");
        }

        RenewalDate = renewalDate;
    }

    public void MarkExpired()
    {
        RenewalDate = DateTime.UtcNow.AddDays(-1); // Artificial expiration
    }

    public bool IsCurrent()
    {
        return RenewalDate.HasValue && RenewalDate.Value > DateTime.UtcNow;
    }
}

