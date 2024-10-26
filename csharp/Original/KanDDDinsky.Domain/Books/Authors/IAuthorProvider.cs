using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Books.Authors;

public interface IAuthorProvider
{
    Task<Author> GetOrCreate(AuthorIdOrData authorIdOrData, CancellationToken ct);
}
