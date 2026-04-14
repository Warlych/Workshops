using Mediator;
using VSA.Contracts;

namespace VSA.Features.Book.EventHandlers;

public sealed class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
{
    public async ValueTask Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"NoteCreatedEvent {notification.Id} created");
    }
}
