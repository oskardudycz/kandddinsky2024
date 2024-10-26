using KanDDDinsky.Books.Entities;
using KanDDDinsky.Books.Publishers;
using KanDDDinsky.Persistence.Books.Mappers;

namespace KanDDDinsky.Persistence.Publishers;

public class PublisherProvider(KanDDDinskyDbContext dbContext): IPublisherProvider
{
    public async Task<Publisher> GetById(PublisherId publisherId, CancellationToken ct) =>
        (await dbContext.Publishers.FindAsync(new object[] { publisherId.Value }, ct))?.Map() ??
        throw new InvalidOperationException();
}
