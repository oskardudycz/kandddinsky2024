using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record RemoveFormatCommand(BookId BookId, Format Format);
