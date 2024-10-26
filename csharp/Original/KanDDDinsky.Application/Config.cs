using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KanDDDinsky.Application.Books;
using KanDDDinsky.Books;
using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Factories;
using KanDDDinsky.Books.Publishers;
using KanDDDinsky.Books.Repositories;
using KanDDDinsky.Persistence;
using KanDDDinsky.Persistence.Authors;
using KanDDDinsky.Persistence.Books.Repositories;
using KanDDDinsky.Persistence.Publishers;

namespace KanDDDinsky.Application;

public static class Config
{
    public static IServiceCollection AddKanDDDinsky(this IServiceCollection services, IConfiguration config) =>
        services
            .AddScoped<IBookFactory, Book.Factory>()
            .AddScoped<IBooksRepository, BooksRepository>()
            .AddScoped<IBooksQueryRepository, BooksQueryRepository>()
            .AddScoped<IBooksService, BooksService>()
            .AddScoped<IBookQueryService, BooksQueryService>()
            .AddScoped<IAuthorProvider, AuthorProvider>()
            .AddScoped<IPublisherProvider, PublisherProvider>()
            .AddDbContext<KanDDDinskyDbContext>();
}
