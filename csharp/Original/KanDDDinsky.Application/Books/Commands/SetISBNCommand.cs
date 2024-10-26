using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record SetISBNCommand(BookId BookId, ISBN ISBN);
