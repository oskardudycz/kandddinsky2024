using KanDDDinsky.Books.Entities;

namespace KanDDDinsky.Application.Books.Commands;

public record ApproveCommand(BookId BookId, CommitteeApproval CommitteeApproval);
