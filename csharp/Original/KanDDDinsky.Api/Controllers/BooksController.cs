using Microsoft.AspNetCore.Mvc;
using KanDDDinsky.Api.Requests;
using KanDDDinsky.Application.Books;
using KanDDDinsky.Application.Books.Commands;
using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Entities;
using KanDDDinsky.Core.Validation;
using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Api.Controllers;

[Route("api/[controller]")]
public class BooksController(IBooksService booksService, IBookQueryService booksQueryService)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateDraft([FromBody] CreateDraftRequest request, CancellationToken ct)
    {
        var bookId = Guid.NewGuid();

        var (title, author, publisherId, edition, genre) = request;

        author.AssertNotNull();

        await booksService.CreateDraft(
            new CreateDraftCommand(
                new BookId(bookId),
                new Title(title.AssertNotEmpty()),
                new AuthorIdOrData(
                    author.AuthorId.HasValue ? new AuthorId(author.AuthorId.Value) : null,
                    author.FirstName != null ? new AuthorFirstName(author.FirstName) : null,
                    author.LastName != null ? new AuthorLastName(author.LastName) : null
                ),
                new PublisherId(publisherId.AssertNotEmpty()),
                new PositiveInt(edition.GetValueOrDefault()),
                genre != null ? new Genre(genre) : null
            ),
            ct
        );

        return Created($"/api/books/{bookId}", bookId);
    }

    [HttpPost("{id}/chapters")]
    public async Task<IActionResult> AddChapter([FromRoute] Guid id, [FromBody] AddChapterRequest request, CancellationToken ct)
    {
        var (title, content) = request;

        await booksService.AddChapter(
            new AddChapterCommand(
                new BookId(id),
                new ChapterTitle(title.AssertNotEmpty()),
                content != null ? new ChapterContent(content): ChapterContent.Empty
            ),
            ct
        );

        return NoContent();
    }

    [HttpPost("{id}/move-to-editing")]
    public async Task<IActionResult> MoveToEditing([FromRoute] Guid id, CancellationToken ct)
    {
        await booksService.MoveToEditing(
            new MoveToEditingCommand(new BookId(id)),
            ct
        );

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindDetailsById([FromRoute] Guid id, CancellationToken ct) =>
        await booksQueryService.FindDetailsById(new BookId(id), ct) is { } result ? Ok(result) : NotFound();
}
