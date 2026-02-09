using Mediator;

namespace ServiceA.Handlers.Commands;

public record CreateOrderCommand(string Goods) : ICommand<Guid>;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    public CreateOrderCommandHandler()
    {
        // dependency injection
    }
    
    public async ValueTask<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // use-case
        // ...
        
        return Guid.CreateVersion7();
    }
}