using KanDDDinsky.Application.Books.Commands;
using KanDDDinsky.Books;
using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Publishers;
using KanDDDinsky.Books.Repositories;
using KanDDDinsky.Books.Services;
using KanDDDinsky.EmployeeCompliance;
using Microsoft.EntityFrameworkCore;

namespace KanDDDinsky.Application.Books;

public interface IEmployeeComplianceRepository
{
    Task<EmployeeCompliance.EmployeeCompliance?> FindById(EmployeeId id, CancellationToken ct);

    Task Update(EmployeeCompliance employeeCompliance, CancellationToken ct);
}

public class EmployeeComplianceRepository: IEmployeeComplianceRepository
{
    public DbContext dbContext { get; }

    public Task<EmployeeCompliance.EmployeeCompliance?> FindById(EmployeeId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task GetAndUpdate(EmployeeId id, Func<ComplianceEvent> handle, CancellationToken ct)
    {
        // Get entity
        var entity = await dbContext.Set<EmployeeCompliance.EmployeeCompliance>().FindAsync([id]);

        var aggregate = Map(entity);

        handle(aggregate);

        var events = employeeCompliance.dequeuePendingEvents();

        foreach (var @event in events)
        {
            Apply(entity, @event);
        }

        dbContext.SaveChangesAsync();
    }

    private void Apply(EmployeeCompliance.EmployeeCompliance? entity, ComplianceEvent @event)
    {
        switch (@event)
        {
            case ComplianceEvent.ComplianceMetEvent:
            {
                dbContext.Set<Certificate>().Add();
                entity.Status = @event.Status;
                break;
            }
        }
    }

    public Task Update(EmployeeCompliance employeeCompliance, CancellationToken ct)
    {
        var events = employeeCompliance.dequeuePendingEvents();

        foreach (var @event in events)
        {
            switch (@event)
            {
                case ComplianceEvent.ComplianceMetEvent:
                {
                    dbContext
                    break;
                }
            }
        }
    }
}

public class EmployeeComplianceService(
    IEmployeeComplianceRepository repository
)
{
    public async Task CompleteComplianceTraining(CompleteComplianceTrainingCommand command, CancellationToken ct)
    {
        await repository.GetAndUpdate(
                                     command.EmployeeId,
                                     (aggregate) => aggregate.CompleteComplianceTraining(command.Training),
                                     ct
                                     ) ??
                                 throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception
    }
}
