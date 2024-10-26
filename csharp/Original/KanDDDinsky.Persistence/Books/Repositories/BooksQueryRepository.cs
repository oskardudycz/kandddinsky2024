using Microsoft.EntityFrameworkCore;
using KanDDDinsky.Books.DTOs;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Books.Repositories;
using KanDDDinsky.Persistence.Books.Mappers;

namespace KanDDDinsky.Persistence.Books.Repositories;

public class BooksQueryRepository(KanDDDinskyDbContext dbContext): IBooksQueryRepository
{
    public Task<BookDetails?> FindDetailsById(BookId bookId, CancellationToken ct) =>
        dbContext.Books
            .AsNoTracking()
            .Include(e => e.Author)
            .Include(e => e.Publisher)
            .Include(e => e.Reviewers)
            .Include(e => e.Chapters)
            .Include(e => e.Translations)
            .Include(e => e.Formats)
            .Where(e => e.Id == bookId.Value)
            .Select(b => b.MapToDetails())
            .SingleOrDefaultAsync(ct);
}
