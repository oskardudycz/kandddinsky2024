# "Slim your aggregates with an Event-Driven approach" workshop at KanDDDinsky 2024

![KanDDDinsky logo](https://kandddinsky.de/img/design/logo_light.svg)

Aggregate is a useful pattern, but it's tricky to get it right, especially when starting. The initial focus on business logic is too often replaced with state obsession.

During the session, I'll show you how an Event-driven approach and a bit of functional composition can help to slim them down.

All of that will be as live refactoring that starts from a typical, bloated implementation and ends with code focused on behaviour and simplicity.

## The what?

Start by thinking about what Aggregate is actually about. How would you define it? What's the goal of using it?

## The how?

You choose! Which language should we code with: C#, Java, or TypeScript?

## The code

To start our workshop, we need to find an implementation that looks valid at first glance but falls into the common mistakes of Aggregate Design. Where can we find it? 

Luckily, we have a perfect candidate for that: our friend Chet Geppetto, who writes precisely such code!

Let's ask our friend! Let's go to https://chatgpt.com/ and type:

> Could you give me a real-world example of the complex DDD aggregate?
> 
> Describe the business rules and invariants. It should have at least ten public and some private properties and several business rules and invariants showing business logic.
> 
> It should have a list of nested data and a few value objects with logic. Use strongly typed identifiers.
> 
> Root aggregate should represent a complex workflow or process. It should express lifetime or state transition and have several methods for running business logic (more than seven).
> 
> It should be more complex than simple ifs and data assignments (the same goes for value objects). So, more complicated business rules. It should show the imperfect outcome of the project that was completed by multiple people over a longer time span.
> 
> Use some random, untypical business use case fitting the above requirement (other than Order, Loan, or Event Management to not get us bored).
> 
> Describe aggregate flow, business rules first and invariants before implementation.
> 
> Don't list or describe properties; just express them in code.
> 
> Start with the main aggregate implementation, then put the rest of the value objects definitions.
> 
> Write the whole code, including business logic and invariant checks. Provide aggregate and value objects as separate code snippets.
> 
> Remember that value objects should also be broken into dedicated snippets. Business logic should be more complex than state assignments and basic ifs representing the imperfection of a real project's outcome. Again, the logic inside methods should be more complicated than if statement.  We should at least 7 business workflows handled by aggregate. Some methods (but not all!) should be publishing domain events.
> 
> Keep in mind that it'll be placed in the Clean/Hexagonal Architecture and has to be stored with Entity Framework. Provide example application layer and repository for storing our aggregate.
>
> The code should look like written by team of average developers inspired by Youtube/LinkedIn influencers advices.
> 
> Provide an example implementation in C# 11 please don't leave any placeholders or comments for further implementation.

Now, go to your LinkedIn profile and add the Prompt Engineering skill and DDD practitioner title!

## The Output

Here's a complex example based on a use case for **"Employee Compliance and Certification Management in a Healthcare Setting"**. This domain has multiple intricacies in tracking employee certifications, compliance requirements, renewal processes, and dependencies based on job roles, which directly affect eligibility for certain tasks. Each employee has various certifications and training records, each with its own renewal and compliance logic.

The aggregate handles workflows around managing these certifications, determining compliance status, and handling expirations, renewals, and notifications, with several intertwined rules, state transitions, and event publishing.

### 1. Aggregate Root: `EmployeeCompliance`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class EmployeeCompliance
{
    public EmployeeId EmployeeId { get; private set; }
    public List<CertificationRecord> Certifications { get; private set; }
    public ComplianceStatus Status { get; private set; }
    public bool IsEligibleForTaskAssignment { get; private set; }
    private DateTime LastStatusUpdate { get; set; }
    private int MissedCompliances { get; set; }

    public EmployeeCompliance(EmployeeId employeeId)
    {
        EmployeeId = employeeId;
        Certifications = new List<CertificationRecord>();
        Status = ComplianceStatus.InProgress;
        IsEligibleForTaskAssignment = false;
        LastStatusUpdate = DateTime.UtcNow;
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
        certificationRecord.Renew(renewalDate);
        UpdateStatus();
    }

    public void CompleteComplianceTraining(ComplianceTraining training)
    {
        if (!training.IsMandatory || Status == ComplianceStatus.Compliant)
        {
            return;
        }
        
        var complianceMet = Certifications.All(c => c.IsCurrent());
        if (complianceMet)
        {
            Status = ComplianceStatus.Compliant;
            PublishEvent(new ComplianceMetEvent(EmployeeId));
        }
        else
        {
            MissedCompliances++;
            if (MissedCompliances > 3)
            {
                Status = ComplianceStatus.NonCompliant;
                PublishEvent(new ComplianceBreachEvent(EmployeeId));
            }
        }

        UpdateTaskEligibility();
        LastStatusUpdate = DateTime.UtcNow;
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
        LastStatusUpdate = DateTime.UtcNow;
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
        IsEligibleForTaskAssignment = Status == ComplianceStatus.Compliant && LastStatusUpdate > DateTime.UtcNow.AddMonths(-6);
    }

    private void PublishEvent(object @event)
    {
        // Assume some event dispatcher is available for publishing domain events
    }
}
```

### 2. Value Object: `CertificationRecord`

```csharp
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
        RenewalDate = DateTime.UtcNow.AddDays(-1);  // Artificial expiration
    }

    public bool IsCurrent()
    {
        return RenewalDate.HasValue && RenewalDate.Value > DateTime.UtcNow;
    }
}
```

### 3. Value Object: `Certification`

```csharp
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
```

### 4. Value Object: `ComplianceTraining`

```csharp
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
```

### 5. Strongly Typed Identifier: `EmployeeId`, `CertificationId`

```csharp
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
```

### 6. Compliance Status Enum

```csharp
public enum ComplianceStatus
{
    Compliant,
    NonCompliant,
    InProgress
}
```

### 7. Domain Events: `ComplianceMetEvent`, `ComplianceBreachEvent`, `CompliancePendingEvent`

```csharp
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
```

### 8. Application Layer and Repository

```csharp
public class EmployeeComplianceRepository
{
    private readonly DbContext _context;

    public EmployeeComplianceRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeCompliance?> GetByIdAsync(EmployeeId employeeId)
    {
        return await _context.Set<EmployeeCompliance>().FindAsync(employeeId);
    }

    public async Task SaveAsync(EmployeeCompliance employeeCompliance)
    {
        _context.Update(employeeCompliance);
        await _context.SaveChangesAsync();
    }
}

public class ComplianceService
{
    private readonly EmployeeComplianceRepository _repository;

    public ComplianceService(EmployeeComplianceRepository repository)
    {
        _repository = repository;
    }

    public async Task ProcessCertificationRenewal(EmployeeId employeeId, CertificationId certificationId, DateTime renewalDate)
    {
        var compliance = await _repository.GetByIdAsync(employeeId);
        compliance?.RenewCertification(certificationId, renewalDate);
        await _repository.SaveAsync(compliance);
    }
}
```

This aggregate captures a range of business rules and complexities, from managing certifications to updating compliance status and eligibility.

## Paste it in your code

Create the new module inside the Domain folder in your selected language:
- [C#](./csharp/Original/KanDDDinsky.Domain/),
- [Java](./java/src/main/java/io/eventdriven/slimdownaggregates/original/domain/),
- [TypeScript](./node.js/src/original/domain/).

## Prerequisitions

1. Clone this repository.
2. Install .NET 8 - https://dotnet.microsoft.com/en-us/download/dotnet/7.0.
3. Install Rider, Visual Studio, VSCode or other preferred IDE.
4. Open [SlimDownAggregates.sln](./csharp/SlimDownAggregates.sln) solution.
5. Run build.
6. To run integration tests you either need to run `docker-compose up` to setup Postgres container. Or set `TEST_IN_MEMORY` environment variable to true if you'd like to run them in memory.

### Java

1. Clone this repository.
2. Install Java JDK 22 (or later) - https://www.oracle.com/java/technologies/downloads/.
3. Install IntelliJ, Eclipse, VSCode or other preferred IDE.
4. Open [java](./java/) folder as project.

### TypeScript

1. Clone this repository.
2. Install Node.js 18 (or later) - https://node.js.org/en/download/ (Or better using NVM).
3. Install VSCode, WebStorm or other preferred IDE.
4. Open [node.js](./node.js/) folder as project.
5. Run `npm run build` to verify that code is compiling.
6. Run `npm run test` to verify if all is working.