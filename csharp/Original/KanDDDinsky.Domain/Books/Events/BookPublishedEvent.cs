using KanDDDinsky.Books.Entities;
using KanDDDinsky.Core.Events;

namespace KanDDDinsky.Books.Events;

public record BookPublishedEvent(BookId BookId, ISBN ISBN, Title Title, Author Author): IDomainEvent;
