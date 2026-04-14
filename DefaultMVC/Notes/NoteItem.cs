namespace DefaultMVC.Notes;

public sealed record NoteItem(
    int Id,
    string Text,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);

