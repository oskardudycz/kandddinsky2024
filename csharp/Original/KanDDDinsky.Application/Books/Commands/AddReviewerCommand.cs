using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record AddReviewerCommand(BookId BookId, Reviewer Reviewer);
