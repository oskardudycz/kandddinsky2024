using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Application.Books.Commands;

public record CreateDraftCommand(
    BookId BookId,
    Title Title,
    AuthorIdOrData Author,
    PublisherId PublisherId,
    PositiveInt Edition,
    Genre? Genre
);
