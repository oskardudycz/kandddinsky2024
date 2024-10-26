using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Books.Publishers;

public interface IPublisherProvider
{
    Task<Publisher> GetById(PublisherId publisherId, CancellationToken ct);
}
