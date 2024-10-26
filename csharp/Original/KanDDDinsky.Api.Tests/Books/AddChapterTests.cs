using KanDDDinsky.Api.Requests;

namespace KanDDDinsky.Api.Tests.Books;

public class AddChapterTests(ApiSpecification api): IClassFixture<ApiSpecification>
{
    [Fact]
    public Task AddsChapter_ForExistingBook() =>
        api.Given(CreatedDraft())
            .When(
                POST,
                URI(ctx => $"/api/books/{ctx.GetCreatedId()}/chapters"),
                BODY(new AddChapterRequest("Chapter 1 - Prolog", null))
            )
            .Then(NO_CONTENT);
}
