namespace KanDDDinsky.Api.Tests.Books;

public class GetBookDetailsByIdTests(ApiSpecification api): IClassFixture<ApiSpecification>
{
    [Fact]
    public Task ReturnsNotFound_ForNonExistingBook() =>
        api.Given()
            .When(
                GET,
                URI($"/api/books/{UnknownBookId}")
            )
            .Then(NOT_FOUND);

    [Fact]
    public Task ReturnsDetails_ForExistingBook() =>
        api.Given(CreatedDraft())
            .When(
                GET,
                URI(ctx => $"/api/books/{ctx.GetCreatedId()}")
            )
            .Then(OK);

    private readonly Guid UnknownBookId = Guid.NewGuid();
    private readonly Guid ExistingBookId = Guid.NewGuid();
}
