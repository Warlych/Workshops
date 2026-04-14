using Mediator;

namespace VSA.PipelineBehaviours;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{DateTime.Now} new request: {message.GetType().Name}");

        return await next(message, cancellationToken);
    }
}
