using KanDDDinsky.Books.Entities;
using KanDDDinsky.Core.Events;

namespace KanDDDinsky.Books.Events;

public record BookMovedToEditingEvent(BookId BookId): IDomainEvent;
