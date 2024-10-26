using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record AddChapterCommand(BookId BookId, ChapterTitle Title, ChapterContent Content);
