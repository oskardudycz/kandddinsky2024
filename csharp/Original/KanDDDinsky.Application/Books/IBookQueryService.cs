using KanDDDinsky.Books.DTOs;
using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books;

public interface IBookQueryService
{
    Task<BookDetails?> FindDetailsById(BookId bookId, CancellationToken ct);
}
