using KanDDDinsky.Books.Entities;
using KanDDDinsky.Core.Events;

namespace KanDDDinsky.Books.Events;

public record ChapterAddedEvent(BookId BookId, Chapter Chapter): IDomainEvent;
