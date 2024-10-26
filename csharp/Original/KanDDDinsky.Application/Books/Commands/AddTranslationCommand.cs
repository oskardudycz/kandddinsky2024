using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record AddTranslationCommand(BookId BookId, Translation Translation);
