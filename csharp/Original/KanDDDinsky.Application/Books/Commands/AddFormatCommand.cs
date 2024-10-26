using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record AddFormatCommand(BookId BookId, Format Format);
