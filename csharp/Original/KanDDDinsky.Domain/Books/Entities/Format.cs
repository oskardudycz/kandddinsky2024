using KanDDDinsky.Core.Validation;
using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record Format
{
    public Format(FormatType formatType, PositiveInt totalCopies, PositiveInt soldCopies)
    {
        FormatType = formatType;
        TotalCopies = totalCopies;
        SoldCopies = soldCopies;

        TotalCopies.AssertGreaterOrEqualThan(SoldCopies);
    }

    public FormatType FormatType { get; }
    public PositiveInt TotalCopies { get; private set; }
    public PositiveInt SoldCopies { get; private set; }

    public void SetTotalCopies(PositiveInt totalCopies) =>
        TotalCopies = totalCopies;

    public void SetSoldCopies(PositiveInt soldCopies) =>
        TotalCopies = soldCopies;

}

public record FormatType(string Value): NonEmptyString(Value);
