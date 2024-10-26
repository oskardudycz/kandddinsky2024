using KanDDDinsky.Core.Validation;

namespace KanDDDinsky.Core.ValueObjects;

public record NonEmptyGuid
{
    protected NonEmptyGuid(Guid value) =>
        Value = value.AssertNotEmpty();

    public Guid Value { get; }
}
