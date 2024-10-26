using KanDDDinsky.Core.ValueObjects;

namespace KanDDDinsky.Books.Entities;

public record CommitteeApproval(bool IsApproved, NonEmptyString Feedback);

