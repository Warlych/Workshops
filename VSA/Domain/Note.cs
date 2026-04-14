namespace VSA.Domain;

public sealed record Note(
    int Id,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);


