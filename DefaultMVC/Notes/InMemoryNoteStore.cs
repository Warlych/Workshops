using System.Collections.Concurrent;
using System.Threading;

namespace DefaultMVC.Notes;

public sealed class InMemoryNoteStore
{
    private readonly ConcurrentDictionary<int, NoteItem> _notes = new();
    private int _nextId;

    public IReadOnlyList<NoteItem> GetAll()
        => _notes.Values.OrderBy(n => n.Id).ToList();

    public NoteItem? GetById(int id)
        => _notes.TryGetValue(id, out var note) ? note : null;

    public NoteItem Add(string text)
    {
        var id = Interlocked.Increment(ref _nextId);
        var now = DateTimeOffset.UtcNow;
        var created = new NoteItem(id, text, now, UpdatedAt: null);
        _notes[id] = created;
        return created;
    }

    public NoteItem? Update(int id, string text)
    {
        while (true)
        {
            if (!_notes.TryGetValue(id, out var existing))
            {
                return null;
            }

            var updated = existing with
            {
                Text = text,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            if (_notes.TryUpdate(id, updated, existing))
            {
                return updated;
            }
        }
    }
}

