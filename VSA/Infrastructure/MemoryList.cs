using VSA.Domain;

namespace VSA.Infrastructure;

public sealed class MemoryList
{
    public List<Note> Notes { get; } = [];
}
