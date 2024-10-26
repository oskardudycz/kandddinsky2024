namespace KanDDDinsky.Api.Tests.Books;

public class MoveToEditingTests(ApiSpecification api): IClassFixture<ApiSpecification>
{
    [Fact]
    public Task MoveToEditing_ForExistingBook() =>
        api.Given(
                CreatedDraft(),
                WithChapter()
            )
            .When(
                POST,
                URI(ctx => $"/api/books/{ctx.GetCreatedId()}/move-to-editing")
            )
            .Then(NO_CONTENT);
}
