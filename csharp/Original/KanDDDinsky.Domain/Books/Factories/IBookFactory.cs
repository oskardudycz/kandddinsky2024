using KanDDDinsky.Books.Entities;
using KanDDDinsky.Books.Services;
using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Factories;

public interface IBookFactory
{
    Book Create(
        BookId bookId,
        Book.State state,
        Title title,
        Author author,
        IKanDDDinsky KanDDDinsky,
        Publisher publisher,
        PositiveInt edition,
        Genre? genre,
        ISBN? isbn,
        DateOnly? publicationDate,
        PositiveInt? totalPages,
        PositiveInt? numberOfIllustrations,
        NonEmptyString? bindingType,
        NonEmptyString? summary,
        CommitteeApproval? committeeApproval,
        List<Reviewer> reviewers,
        List<Chapter> chapters,
        List<Translation> translations,
        List<Format> formats
    );
}
