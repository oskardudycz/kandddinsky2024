using KanDDDinsky.Books.DTOs;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Books.Repositories;

namespace KanDDDinsky.Application.Books;

public class BooksQueryService(IBooksQueryRepository repository): IBookQueryService
{
    public Task<BookDetails?> FindDetailsById(BookId bookId, CancellationToken ct) =>
        repository.FindDetailsById(bookId, ct);
}
