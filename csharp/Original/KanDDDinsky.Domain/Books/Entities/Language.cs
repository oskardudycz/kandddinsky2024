using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Language(LanguageId Id, LanguageName Name);
public record LanguageId(Guid Value): NonEmptyGuid(Value);
public record LanguageName(string Value): NonEmptyString(Value);

