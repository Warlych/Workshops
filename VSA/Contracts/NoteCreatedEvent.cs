using Mediator;

namespace VSA.Contracts;

public record NoteCreatedEvent(int Id) : INotification;
