using Mediator;
using VSA.Common;
using VSA.Contracts;
using VSA.Domain;
using VSA.Infrastructure;

namespace VSA.Features.Notes.Create;

public class CreateNoteValidator
{
    public bool Validate()
    {
        return true;
    }
}

public record CreateNoteCommand(string Text) : ICommand<Note>;

public sealed class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, Note>
{
    private readonly MemoryList _memoryList;
    private readonly IPublisher _publisher;

    public CreateNoteCommandHandler(MemoryList memoryList, IPublisher publisher)
    {
        _memoryList = memoryList;
        _publisher = publisher;
    }

    public async ValueTask<Note> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        if (new CreateNoteValidator().Validate())
        {
            
        }
        
        var rnd = new Random();

        var note = new Note(rnd.Next(1, 100), command.Text, DateTime.UtcNow, null);
        
        _memoryList.Notes.Add(note);
        
        await _publisher.Publish(new NoteCreatedEvent(note.Id), cancellationToken);
        
        return note;
    }
}

public class CreateNoteEndpoint : IEndpointConfiguration
{
    public void Map(WebApplication app)
    {
        app.MapPost("api/notes", async (CreateNoteCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request);

            return result;
        });
    }
}