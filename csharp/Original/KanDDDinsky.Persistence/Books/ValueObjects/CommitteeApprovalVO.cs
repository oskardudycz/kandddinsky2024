namespace KanDDDinsky.Persistence.Books.ValueObjects;

public class CommitteeApprovalVO(bool isApproved, string feedback)
{
    public bool IsApproved { get; } = isApproved;
    public string Feedback { get; } = feedback;
}

