using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record BookId(Guid Value) : NonEmptyGuid(Value);
