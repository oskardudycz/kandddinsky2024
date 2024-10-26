using KanDDDinsky.Books.DTOs;
using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Books.Repositories;

public interface IBooksQueryRepository
{
    Task<BookDetails?> FindDetailsById(BookId bookId, CancellationToken ct);
}
