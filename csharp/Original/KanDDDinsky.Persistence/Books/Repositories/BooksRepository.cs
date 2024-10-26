using Microsoft.EntityFrameworkCore;
using KanDDDinsky.Books;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Books.Factories;
using KanDDDinsky.Books.Repositories;
using KanDDDinsky.Persistence.Books.Mappers;
using KanDDDinsky.Persistence.Core.Repositories;

namespace KanDDDinsky.Persistence.Books.Repositories;

public class BooksRepository(KanDDDinskyDbContext dbContext, IBookFactory bookFactory):
    EntityFrameworkRepository<Book, BookId, BookEntity, KanDDDinskyDbContext>(dbContext), IBooksRepository
{
    protected override IQueryable<BookEntity> Includes(DbSet<BookEntity> query) =>
        query.Include(e => e.Author)
            .Include(e => e.Publisher)
            .Include(e => e.Reviewers)
            .Include(e => e.Chapters)
            .Include(e => e.Translations)
            .Include(e => e.Formats);

    protected override Book MapToAggregate(BookEntity entity) =>
        entity.MapToAggregate(bookFactory);

    protected override BookEntity MapToEntity(Book aggregate) =>
        aggregate.MapToEntity(DbContext);

    protected override void UpdateEntity(BookEntity entity, Book aggregate) =>
        entity.UpdateFrom(aggregate);
}
