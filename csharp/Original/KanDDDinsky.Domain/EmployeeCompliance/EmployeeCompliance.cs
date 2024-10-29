namespace KanDDDinsky.EmployeeCompliance;

using System;
using System.Collections.Generic;
using System.Linq;

public class EmployeeCompliance
{
    private List<CertificationRecord> Certifications { get; private set; }
    private ComplianceStatus Status { get; private set; }
    private bool IsEligibleForTaskAssignment { get; private set; }
    private int MissedCompliances { get; set; }

    public EmployeeCompliance(EmployeeId employeeId)
    {
        Certifications = new List<CertificationRecord>();
        Status = ComplianceStatus.InProgress;
        IsEligibleForTaskAssignment = false;
        MissedCompliances = 0;
    }

    public void AddCertification(Certification certification, DateTime dateIssued)
    {
        if (Certifications.Any(c => c.Certification == certification))
        {
            throw new InvalidOperationException("Certification already exists.");
        }
        Certifications.Add(new CertificationRecord(certification, dateIssued));
        UpdateStatus();
    }

    public void RenewCertification(CertificationId certificationId, DateTime renewalDate)
    {
        var certificationRecord = Certifications.FirstOrDefault(c => c.Certification.Id == certificationId);
        if (certificationRecord == null)
        {
            throw new InvalidOperationException("Certification not found.");
        }
        // certificationRecord.Renew(renewalDate);
        // UpdateStatus();
    }

    public void CompleteComplianceTraining(ComplianceTraining training)
    {
        if (!training.IsMandatory || Status == ComplianceStatus.Compliant)
            return;

        var complianceMet = Certifications.All(c => c.IsCurrent());
        if (complianceMet)
        {
            // Status = ComplianceStatus.Compliant;
            PublishEvent(new ComplianceMetEvent(EmployeeId));
        }
        else
        {
            MissedCompliances++;
            if (MissedCompliances > 3)
            {
                // Status = ComplianceStatus.NonCompliant;
                PublishEvent(new ComplianceBreachEvent(EmployeeId));
            }
        }

        // UpdateTaskEligibility();
        // LastStatusUpdate = DateTime.UtcNow;
    }

    public void HandleTaskAssignmentRequest()
    {
        if (Status != ComplianceStatus.Compliant || !IsEligibleForTaskAssignment)
        {
            throw new InvalidOperationException("Employee is not eligible for task assignment.");
        }
        IsEligibleForTaskAssignment = false;
    }

    public void RecalculateComplianceStatus()
    {
        var compliant = Certifications.All(c => c.IsCurrent());
        Status = compliant ? ComplianceStatus.Compliant : ComplianceStatus.NonCompliant;
        PublishEvent(compliant ? new ComplianceMetEvent(EmployeeId) : new ComplianceBreachEvent(EmployeeId));
    }

    public void ExpireCertification(CertificationId certificationId)
    {
        var certificationRecord = Certifications.FirstOrDefault(c => c.Certification.Id == certificationId);
        if (certificationRecord == null)
        {
            throw new InvalidOperationException("Certification not found.");
        }
        certificationRecord.MarkExpired();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (Certifications.Any(c => c.IsExpired))
        {
            Status = ComplianceStatus.NonCompliant;
        }
        else if (Certifications.All(c => c.IsCurrent()))
        {
            Status = ComplianceStatus.Compliant;
            IsEligibleForTaskAssignment = true;
        }
        else
        {
            Status = ComplianceStatus.InProgress;
            IsEligibleForTaskAssignment = false;
        }

        PublishEvent(Status == ComplianceStatus.Compliant ? new ComplianceMetEvent(EmployeeId) : new CompliancePendingEvent(EmployeeId));
    }

    private void UpdateTaskEligibility()
    {
        IsEligibleForTaskAssignment = Status == ComplianceStatus.Compliant;// && LastStatusUpdate > DateTime.UtcNow.AddMonths(-6);
    }

    private void PublishEvent(object @event)
    {
        // Assume some event dispatcher is available for publishing domain events
    }
}
