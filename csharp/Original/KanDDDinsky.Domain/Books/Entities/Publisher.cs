using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Publisher(PublisherId Id, PublisherName Name);
public record PublisherId(Guid Value) : NonEmptyGuid(Value);
public record PublisherName(string Value) : NonEmptyString(Value);
