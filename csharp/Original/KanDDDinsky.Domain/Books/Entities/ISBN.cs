using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record ISBN(string Value): NonEmptyString(Value);
