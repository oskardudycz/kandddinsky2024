using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Persistence.Books.Mappers;

namespace KanDDDinsky.Persistence.Authors;

public class AuthorProvider(KanDDDinskyDbContext dbContext): IAuthorProvider
{
    public async Task<Author> GetOrCreate(AuthorIdOrData authorIdOrData, CancellationToken ct)
    {
        if (authorIdOrData.AuthorId != null)
            return (await dbContext.Authors.FindAsync(authorIdOrData.AuthorId, ct))?.Map() ??
                   throw new InvalidOperationException();

        var (firstName, lastName) = authorIdOrData.Data;

        var author = new AuthorEntity
        {
            FirstName = firstName.Value, LastName = lastName.Value
        };

        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync(ct);

        return author.Map();
    }
}
