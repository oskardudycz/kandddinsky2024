using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Title(string Value): NonEmptyString(Value);
