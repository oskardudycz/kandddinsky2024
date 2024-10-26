using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Genre(string Value): NonEmptyString(Value);

