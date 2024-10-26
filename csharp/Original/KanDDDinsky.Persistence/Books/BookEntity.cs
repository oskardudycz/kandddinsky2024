using KanDDDinsky.Persistence.Authors;
using KanDDDinsky.Persistence.Books.Entities;
using KanDDDinsky.Persistence.Books.ValueObjects;
using KanDDDinsky.Persistence.Publishers;
using KanDDDinsky.Persistence.Reviewers;

namespace KanDDDinsky.Persistence.Books;

public class BookEntity
{
    public enum State { Writing, Editing, Printing, Published, OutOfPrint }

    public required Guid Id { get; set; }
    public required State CurrentState { get; set; }
    public required string Title { get; set; }
    public required AuthorEntity Author { get; set; }
    public required PublisherEntity Publisher { get; set; }
    public required int Edition { get; set; }
    public string? Genre { get; set; }
    public string? ISBN { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public int? TotalPages { get; set; }
    public int? NumberOfIllustrations { get; set; }
    public string? BindingType { get; set; }
    public string? Summary { get; set; }
    public CommitteeApprovalVO? CommitteeApproval { get; set; }
    public required List<ReviewerEntity> Reviewers { get; set; } = new();
    public required List<ChapterEntity> Chapters { get; set; } = new();
    public required List<TranslationVO> Translations { get; set; } = new();
    public required List<FormatEntity> Formats { get; set; } = new();
}
