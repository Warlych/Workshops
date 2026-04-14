var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<DefaultMVC.Notes.InMemoryNoteStore>();
builder.Services.AddScoped<DefaultMVC.Notes.INoteService, DefaultMVC.Notes.NoteService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

var notes = app.MapGroup("/api/min/notes");

notes.MapGet("/", (DefaultMVC.Notes.INoteService service) => service.GetAll());

notes.MapGet("/{id:int}", (int id, DefaultMVC.Notes.INoteService service) =>
{
    var note = service.GetById(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

notes.MapPost("/", (DefaultMVC.Notes.CreateNoteRequest request, DefaultMVC.Notes.INoteService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { message = "Text is required." });
    }

    var created = service.Create(request);
    return Results.Created($"/api/min/notes/{created.Id}", created);
});

notes.MapPut("/{id:int}", (int id, DefaultMVC.Notes.UpdateNoteRequest request, DefaultMVC.Notes.INoteService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { message = "Text is required." });
    }

    var updated = service.Update(id, request);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

app.Run();
