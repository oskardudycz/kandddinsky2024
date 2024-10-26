using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Books.Services;

public interface IKanDDDinsky
{
    bool IsGenreLimitReached(Genre genre);
}

