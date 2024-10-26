using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public class Chapter(ChapterNumber number, ChapterTitle title, ChapterContent content)
{
    public ChapterNumber Number { get; } = number;
    public ChapterTitle Title { get; private set; } = title;
    public ChapterContent Content { get; private set; } = content;

    public void ChangeTitle(ChapterTitle title) =>
        Title = title;
    public void ChangeContent(ChapterContent content) =>
        Content = content;
}

public record ChapterNumber(int Value): PositiveInt(Value);


public record ChapterTitle(string Value): NonEmptyString(Value);


public record ChapterContent(string Value)
{
    public static readonly ChapterContent Empty = new(string.Empty);
};
