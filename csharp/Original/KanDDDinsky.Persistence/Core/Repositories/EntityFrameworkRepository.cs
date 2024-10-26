using Microsoft.EntityFrameworkCore;
using KanDDDinsky.Core.Aggregates;
using KanDDDinsky.Core.Events;
using KanDDDinsky.Core.ValueObjects;
using KanDDDinsky.Persistence.Core.Outbox;

namespace KanDDDinsky.Persistence.Core.Repositories;

public abstract class EntityFrameworkRepository<TAggregate, TKey, TEntity, TDbContext>(TDbContext dbContext)
    where TAggregate : Aggregate<TKey>
    where TKey : NonEmptyGuid
    where TDbContext : DbContext
    where TEntity : class
{
    protected readonly TDbContext DbContext = dbContext;

    public async Task<TAggregate?> FindById(TKey bookId, CancellationToken ct)
    {
        var entity = await Includes(DbContext.Set<TEntity>())
            .AsNoTracking()
            .SingleOrDefaultAsync(ct);

        return entity != null ? MapToAggregate(entity): default;
    }

    public async Task Add(TAggregate aggregate, CancellationToken ct)
    {
        DbContext.Set<TEntity>().Add(MapToEntity(aggregate));
        ScheduleOutbox(aggregate.DomainEvents);

        await DbContext.SaveChangesAsync(ct);
        aggregate.ClearEvents();
    }

    public async Task Update(TAggregate aggregate, CancellationToken ct)
    {
        var entity = await DbContext.Set<TEntity>().FindAsync(
            new object?[] { aggregate.Id.Value },
            cancellationToken: ct
        ) ?? throw new InvalidOperationException();

        UpdateEntity(entity, aggregate);
        ScheduleOutbox(aggregate.DomainEvents);

        await DbContext.SaveChangesAsync(ct);
        aggregate.ClearEvents();
    }

    private void ScheduleOutbox(IEnumerable<IDomainEvent> events)
    {
        var messages = events
            .Select(OutboxMessageEntity.From);

        DbContext.Set<OutboxMessageEntity>().AddRange(messages);
    }

    protected virtual IQueryable<TEntity> Includes(DbSet<TEntity> query) =>
        query;

    protected abstract TAggregate MapToAggregate(TEntity entity);

    protected abstract TEntity MapToEntity(TAggregate aggregate);

    protected abstract void UpdateEntity(TEntity entity, TAggregate aggregate);
}
