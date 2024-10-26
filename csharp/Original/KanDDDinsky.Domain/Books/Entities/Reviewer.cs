using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Reviewer(ReviewerId Id, ReviewerName Name);
public record ReviewerId(Guid Value) : NonEmptyGuid(Value);
public record ReviewerName(string Value) : NonEmptyString(Value);

