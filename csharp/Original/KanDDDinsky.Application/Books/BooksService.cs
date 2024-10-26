using KanDDDinsky.Application.Books.Commands;
using KanDDDinsky.Books;
using KanDDDinsky.Books.Authors;
using KanDDDinsky.Books.Publishers;
using KanDDDinsky.Books.Repositories;
using KanDDDinsky.Books.Services;

namespace KanDDDinsky.Application.Books;

public class BooksService(
    IBooksRepository repository,
    IAuthorProvider authorProvider,
    IPublisherProvider publisherProvider
 ): IBooksService
{
    public async Task CreateDraft(CreateDraftCommand command, CancellationToken ct)
    {
        var (bookId, title, author, publisherId, edition, genre) = command;

        var book = Book.CreateDraft(
            bookId,
            title,
            await authorProvider.GetOrCreate(author, ct),
            (null as IKanDDDinsky)!, //TODO: Consider making it smarter
            await publisherProvider.GetById(publisherId, ct),
            edition,
            genre
        );

        await repository.Add(book, ct);
    }

    public async Task AddChapter(AddChapterCommand command, CancellationToken ct)
    {
        var (bookId, chapterTitle, chapterContent) = command;

        var book = await repository.FindById(bookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.AddChapter(chapterTitle, chapterContent);

        await repository.Update(book, ct);
    }

    public async Task MoveToEditing(MoveToEditingCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.MoveToEditing();

        await repository.Update(book, ct);
    }

    public async Task AddTranslation(AddTranslationCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.AddTranslation(command.Translation);

        await repository.Update(book, ct);
    }

    public async Task AddFormat(AddFormatCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.AddFormat(command.Format);

        await repository.Update(book, ct);
    }

    public async Task RemoveFormat(RemoveFormatCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.RemoveFormat(command.Format);

        await repository.Update(book, ct);
    }

    public async Task AddReviewer(AddReviewerCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.AddReviewer(command.Reviewer);

        await repository.Update(book, ct);
    }

    public async Task Approve(ApproveCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.Approve(command.CommitteeApproval);

        await repository.Update(book, ct);
    }

    public async Task SetISBN(SetISBNCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.SetISBN(command.ISBN);

        await repository.Update(book, ct);
    }

    public async Task MoveToPublished(MoveToPublishedCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.MoveToPublished();

        await repository.Update(book, ct);
    }

    public async Task MoveToPrinting(MoveToPrintingCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.MoveToPrinting();

        await repository.Update(book, ct);
    }

    public async Task MoveToOutOfPrint(MoveToOutOfPrintCommand command, CancellationToken ct)
    {
        var book = await repository.FindById(command.BookId, ct) ??
                   throw new InvalidOperationException(); // TODO: Add Explicit Not Found exception

        book.MoveToOutOfPrint();

        await repository.Update(book, ct);
    }
}
