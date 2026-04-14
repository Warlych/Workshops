using DefaultMVC.Notes;
using Microsoft.AspNetCore.Mvc;

namespace DefaultMVC.Controllers;

[ApiController]
[Route("api/notes")]
public sealed class NotesController : ControllerBase
{
    private readonly INoteService _service;

    public NotesController(INoteService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<NoteItem>> GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<NoteItem> GetById(int id)
    {
        var note = _service.GetById(id);
        return note is null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public ActionResult<NoteItem> Create([FromBody] CreateNoteRequest request)
    {
        var created = _service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult<NoteItem> Update(int id, [FromBody] UpdateNoteRequest request)
    {
        var updated = _service.Update(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }
}

