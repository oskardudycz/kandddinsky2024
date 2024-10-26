using KanDDDinsky.Core.Validation;

namespace KanDDDinsky.Core.ValueObjects;

public record NonEmptyString
{
    public NonEmptyString(string value) =>
        Value = value.AssertNotEmpty();

    public string Value { get; }
}
