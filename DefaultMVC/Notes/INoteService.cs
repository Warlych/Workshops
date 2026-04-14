namespace DefaultMVC.Notes;

public interface INoteService
{
    IReadOnlyList<NoteItem> GetAll();
    NoteItem? GetById(int id);
    NoteItem Create(CreateNoteRequest request);
    NoteItem? Update(int id, UpdateNoteRequest request);
}

