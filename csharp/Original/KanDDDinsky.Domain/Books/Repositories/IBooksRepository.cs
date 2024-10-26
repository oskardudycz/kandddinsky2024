using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Books.Repositories;

public interface IBooksRepository
{
    Task<Book?> FindById(BookId bookId, CancellationToken ct);

    Task Add(Book book, CancellationToken ct);

    Task Update(Book book, CancellationToken ct);
}
