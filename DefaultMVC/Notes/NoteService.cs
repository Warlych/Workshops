namespace DefaultMVC.Notes;

public sealed class NoteService : INoteService
{
    private readonly InMemoryNoteStore _store;

    public NoteService(InMemoryNoteStore store)
    {
        _store = store;
    }

    public IReadOnlyList<NoteItem> GetAll()
        => _store.GetAll();

    public NoteItem? GetById(int id)
        => _store.GetById(id);

    public NoteItem Create(CreateNoteRequest request)
        => _store.Add(request.Text.Trim());

    public NoteItem? Update(int id, UpdateNoteRequest request)
        => _store.Update(id, request.Text.Trim());
}

